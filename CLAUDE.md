# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Solution layout

`DraxClient.sln` contains four code projects plus a Visual Studio installer project. All C# projects target **.NET 8** with `Nullable` and `ImplicitUsings` enabled.

| Project | Folder | Type | Purpose |
|---|---|---|---|
| `DraxClient` | `Drax360Client/` | WinExe, `net8.0-windows`, WinForms | The shipped tray app (`AssemblyName=DraxClient`). Root namespace `DraxClient`. |
| `Read AMX File` | `Read AMX File/` | Console, `net8.0` | Standalone utility — reads a Unix-epoch timestamp at byte offset 44 of a `.GEN` panel config file. Sample `.GEN` files are copied to output. |
| `MQTT Test` | `MQTT Test/` | WinExe, WinForms | Scratch harness using `MQTTnet 5.0.1` + `MQTTnet.Extensions.ManagedClient 4.3.7`. Namespace `MQTT_Test`. |
| `MQTTResponder` | `MQTTResponder/` | Console, `net8.0` | Scratch MQTT subscriber/publisher against `broker.hivemq.com`. |
| `DraxClientSetup` | `DraxClientSetup/` | `.vdproj` installer | Builds only in classic VS (Debug/Release only — no Any CPU). Not an SDK-style project. |

`Drax360Client/DraxClient.csproj` is the folder-name / project-name mismatch to watch for: the folder is `Drax360Client` but the project and assembly are both `DraxClient`.

## Build / run

Solution-level build targets `Any CPU`. From the repo root:

```sh
dotnet build DraxClient.sln                       # build everything (SDK projects)
dotnet build Drax360Client/DraxClient.csproj -c Release
dotnet run --project Drax360Client                # run the main client
dotnet run --project "Read AMX File"
```

There is **no test project**, no lint config, and no CI. `DraxClientSetup.vdproj` requires Visual Studio to build; `dotnet` cannot produce the installer.

## How the main client works

`DraxClient` is a headless-by-default WinForms app built around `HiddenAppContext` (`Program.cs`). `Main` calls `AllocConsole()` so a debug console is visible alongside the UI. A `debug` flag in `HiddenAppContext` currently forces the startup form (`frmSetup`) to show — production behaviour is "hidden until summoned via pipe."

### Two named pipes, opposite directions

The client is **both a pipe server and a pipe client**, talking to an external Drax service (not in this repo):

- **`DraxTechnologyPipeReturn`** — client hosts this. The external service writes control messages here to make the UI do things. Handled in `PipeManager.HandlePipeCommand` (`Drax360Client/PipeManager.cs`). Known commands:
  - `|NWM:TBSHOW` / `|NWM:TBHIDE` — show/hide `frmTestBox`
  - `|NWM:SHOWABOUT` — show `frmAbout`
  - `|GEN:SETUPSHOW` — show `frmSetup`
  - `|NWM:CLOSEALLWINDOWS` — currently a no-op
- **`DraxTechnologyPipeSend`** — client connects to this as a client. Every form that needs to read/write settings or issue a panel action uses a local `sendcmd(...)` helper (duplicated across `frmSetup.cs`, `frmTestBox.cs`, `frmprimary.cs`) which opens a `NamedPipeClientStream`, writes one message, reads one message back, closes. `|` (constant `kpipedelim`) is both the command separator and the argument separator.

Common outbound commands seen across forms: `SETTINGSGET|SECTION,KEY`, `SETTINGSSET|SECTION,KEY,VALUE`, `SETTINGSSAVE`, `SETTINGSRELOAD`, `GetPanelType`, `GETCOMMPORTSTATUS|COMx`, `ServiceRestart`, and panel action verbs (`Evacuate`, `Alert`, `Reset`, `Silence`, `DisableDevice`, `EnableDevice`, `DisableZone`, …) with `node,loop,zone,ip` arguments. When adding a new setting/action, follow this pipe convention rather than inventing a new transport.

All UI mutation from pipe callbacks goes through `_mainForm.Invoke(...)` — the pipe listen loop runs on a background `Task`, so never touch forms directly from `HandleClient`. `PipeManager.SetMainForm` must be called before `Start()`.

### Panel-type-driven UI

The client supports multiple fire panel families. `frmSetup.LoadFormData()` calls `GetPanelType` over the pipe and then adds exactly one panel-specific tab (`tprsm`, `tpemail`, `tpadvanced`, `tbGent`) — **all four tabs are removed from the designer layout at startup** and re-added conditionally, so designer-time tab order is not the runtime order. Known panel type strings: `GENT`, `ADVANCED`, `RSM`, `EMAIL`, `MOLREYZX`, `MORLEYMAX`, `NOTIFIER`, `PEARL`, `SYNCRO`, `TAKTIS`. Each type reads a different offset setting key (`AMX1OFFSET` vs `GIAMX1OFFSET` vs `SETUP,AMX1OFFSET` vs `PANEL1,AMX1OFFSET`) — check `LoadFormData`'s switch before assuming a key.

### Panel-specific modules

`Drax360Client/Panels/` houses panel-family code that would otherwise bloat `frmSetup`:

- `Panels/Email/` — `PanelEmailClient` persists `Group`/`Address` data as JSON at `%AppData%\DraxClient\emailgroups.json` (note: writes the user's roaming AppData, not the install dir). Edited via `frmemailgroups` / `frmemailgroup`.
- `Panels/RSM/` — RSM module (TCP/IP gateway device) discovery and configuration. `frmdiscovery.cs` defines two legacy-ordered enums (`optSetGet`, `cmdToPanel`) whose integer values are wire-protocol identifiers — **do not renumber**. `tcp-listener-form.cs` lives in namespace `TcpListenerApp` (not `DraxClient.Panels.RSM`).

### Styling

`frmSetup` applies a custom flat/modern look at runtime via `ApplyModernStyling` (owner-drawn tabs, rounded "card" panels tagged `Tag="card"`, palette constants `clr*`). If a new control needs the same treatment, tag it and let the recursive `GetAllControls` sweep pick it up rather than styling inline.

## Things that will bite you

- `Program.cs` currently boots `frmSetup` as the main form with `debug = true`. If you need the production tray-only behaviour, switch to `frmprimary` and set `debug = false`.
- `sendcmd` is duplicated in `frmSetup`, `frmTestBox`, and `frmprimary` with subtly different encodings (`Encoding.Default` vs UTF-8 on the server side in `PipeManager`). Match the style of the file you are editing.
- `MQTTResponder/Program.cs` has a stray trailing `}` (line 54) — it does not currently build cleanly. Treat that project as scratch.
- `cbBaudRate` in `frmSetup.LoadFormData` has copy-paste bugs in the `Value` column (1200→"600", 2400→"1200"). Leave alone unless fixing intentionally — the external service may depend on the legacy mapping.
- `.GEN` files in `Read AMX File/` are test fixtures, not generated output — do not delete.
