using System.Runtime.InteropServices;

namespace Drax360Client
{

    [System.Security.SuppressUnmanagedCodeSecurity]
    public static class NetManUnsafe
    {
        private const string kdll = "Gen_Netman_x64.dll";

            [DllImport(kdll, EntryPoint = "GetEventLastTime", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
            extern public static int GetEventLastTime(int EventNumber);
            [DllImport(kdll, EntryPoint = "GetInput", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
            extern public static short GetInput(int EventNumber);
            [DllImport(kdll, EntryPoint = "GetInputType", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
            extern public static short GetInputType(int EventNumber);
            [DllImport(kdll, EntryPoint = "GetNode", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
            extern public static short GetNode(int EventNumber);
            [DllImport(kdll, EntryPoint = "GetNodeIgnore", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
            extern public static short GetNodeIgnore(short Node);
            [DllImport(kdll, EntryPoint = "GetNodeLocation", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
            extern public static short GetNodeLocation(short Node, [MarshalAs(UnmanagedType.VBByRefStr)] ref string DestString);
            [DllImport(kdll, EntryPoint = "GetNodeOnline", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
            extern public static short GetNodeOnline(short Node);
            //UPGRADE_TODO: (1050) Structure DLLDATA may require marshalling attributes to be passed as an argument in this Declare statement. More Information: https://docs.mobilize.net/vbuc/ewis/todos#id-1050
            [DllImport(kdll, EntryPoint = "GetNWMData", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
            extern public static short GetNWMData([MarshalAs(UnmanagedType.VBByRefStr)] ref string FileName, short Index, ref StructuresUnsafe.DLLDATA LongArray, [MarshalAs(UnmanagedType.VBByRefStr)] ref string DestString, [MarshalAs(UnmanagedType.VBByRefStr)] ref string ExText, [MarshalAs(UnmanagedType.VBByRefStr)] ref string ExText2);
            [DllImport(kdll, EntryPoint = "GetNWMNumber", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
            extern public static short GetNWMNumber([MarshalAs(UnmanagedType.VBByRefStr)] ref string FileName);
            [DllImport(kdll, EntryPoint = "GetOperatorNameText", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
            extern public static short GetOperatorNameText(int li, [MarshalAs(UnmanagedType.VBByRefStr)] ref string s1);
            [DllImport(kdll, EntryPoint = "GetTimeString", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
            extern public static short GetTimeString(int l1, [MarshalAs(UnmanagedType.VBByRefStr)] ref string s1);
            [DllImport(kdll, EntryPoint = "GetZone", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
            extern public static short GetZone(int EventNumber);
            [DllImport(kdll, EntryPoint = "InitialiseNWM", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
            extern public static void InitialiseNWM([MarshalAs(UnmanagedType.VBByRefStr)] ref string Path);
            [DllImport(kdll, EntryPoint = "LogMessageToAMX1", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
            extern public static void LogMessageToAMX1(int EventType, int EventNumber, [MarshalAs(UnmanagedType.VBByRefStr)] ref string Text, [MarshalAs(UnmanagedType.VBByRefStr)] ref string TransferFile, int OpHandle);
            [DllImport(kdll, EntryPoint = "MakeInputNumber", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
            extern public static int MakeInputNumber(short Node, short Zone, short InputN, short InputType);
            [DllImport(kdll, EntryPoint = "NodeSetIgnore", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
            extern public static void NodeSetIgnore(short Node, short Ignore, int OpHandle);
            [DllImport(kdll, EntryPoint = "NodeSetOnline", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
            extern public static void NodeSetOnline(short Node, short Online, int OpHandle);
            [DllImport(kdll, EntryPoint = "NwmForceEvmAttribute", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
            extern public static void NwmForceEvmAttribute([MarshalAs(UnmanagedType.VBByRefStr)] ref string TransferFile, int EventNumber, short AttributeBit, short OnOff);
            [DllImport(kdll, EntryPoint = "SendAlarmToAMX1", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
            extern public static short SendAlarmToAMX1(short AlarmType, int EventNumber, int LongTime, short IntegerPar, [MarshalAs(UnmanagedType.VBByRefStr)] ref string StringPar, [MarshalAs(UnmanagedType.VBByRefStr)] ref string StringPar2, [MarshalAs(UnmanagedType.VBByRefStr)] ref string StringPar3, [MarshalAs(UnmanagedType.VBByRefStr)] ref string TxFile);
            [DllImport(kdll, EntryPoint = "SendResetToAMX1", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
            extern public static short SendResetToAMX1(short AlarmType, int EventNumber, int LongTime, short IntegerPar, [MarshalAs(UnmanagedType.VBByRefStr)] ref string StringPar, [MarshalAs(UnmanagedType.VBByRefStr)] ref string StringPar2, [MarshalAs(UnmanagedType.VBByRefStr)] ref string StringPar3, [MarshalAs(UnmanagedType.VBByRefStr)] ref string TxFile);
            [DllImport(kdll, EntryPoint = "SetNWMDDEchannel", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
            extern public static void SetNWMDDEchannel(short DDEChannel);
            [DllImport(kdll, EntryPoint = "SetupNWM", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
            extern public static void SetupNWM(short giAmx1Offset);
            [DllImport(kdll, EntryPoint = "WriteNWMData", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
            extern public static void WriteNWMData([MarshalAs(UnmanagedType.VBByRefStr)] ref string FileName, int EventType, int EventNumber, ref int LongArray, [MarshalAs(UnmanagedType.VBByRefStr)] ref string TextParameter, [MarshalAs(UnmanagedType.VBByRefStr)] ref string TextParameter2, [MarshalAs(UnmanagedType.VBByRefStr)] ref string TextParameter3, short OnOff);
        }
    }
