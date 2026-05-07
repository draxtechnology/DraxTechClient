# Regenerates Files.wxs from the contents of Drax360Client\bin\Debug\net8.0-windows.
# Run this whenever a NuGet dependency changes or .NET 8 brings in different
# runtime artifacts. Build the client first (dotnet build) so bin is populated.
#
#   pwsh DraxClientSetup\Regenerate-Files.ps1

Set-Location $PSScriptRoot
$bin = Join-Path $PSScriptRoot '..\Drax360Client\bin\Debug\net8.0-windows'
if (-not (Test-Path $bin)) { throw "$bin not found - run dotnet build first." }

# Skip files already declared explicitly in Product.wxs (the EXE) and pdb symbols.
$rootFiles = Get-ChildItem $bin -File | Where-Object {
  $_.Name -ne 'DraxClient.exe' -and
  $_.Name -ne 'DraxClient.pdb'
} | Sort-Object Name

function Sanitize($name) { ($name -replace '[^A-Za-z0-9_]', '_') }
function StableGuid($s) {
  $sha = [System.Security.Cryptography.SHA1]::Create()
  $hash = $sha.ComputeHash([System.Text.Encoding]::UTF8.GetBytes($s))
  $g = [byte[]]::new(16)
  [Array]::Copy($hash, 0, $g, 0, 16)
  ([System.Guid]::new($g)).ToString().ToUpper()
}

$sb = New-Object System.Text.StringBuilder
[void]$sb.AppendLine('<?xml version="1.0" encoding="UTF-8"?>')
[void]$sb.AppendLine('<Wix xmlns="http://wixtoolset.org/schemas/v4/wxs">')
[void]$sb.AppendLine('  <Fragment>')
[void]$sb.AppendLine('    <ComponentGroup Id="DependencyFiles" Directory="INSTALLFOLDER">')
foreach ($f in $rootFiles) {
  $safe = Sanitize $f.Name
  $guid = StableGuid("DraxClientSetup:$($f.Name)")
  [void]$sb.AppendLine("      <Component Id=`"cmp_$safe`" Guid=`"$guid`">")
  [void]$sb.AppendLine("        <File Id=`"fil_$safe`" Source=`"`$(var.DraxClient.TargetDir)$($f.Name)`" KeyPath=`"yes`" />")
  [void]$sb.AppendLine('      </Component>')
}
[void]$sb.AppendLine('    </ComponentGroup>')
[void]$sb.AppendLine('  </Fragment>')
[void]$sb.AppendLine('</Wix>')

Set-Content -Path (Join-Path $PSScriptRoot 'Files.wxs') -Value $sb.ToString() -Encoding UTF8 -NoNewline
"Wrote Files.wxs: deps=$($rootFiles.Count)"
