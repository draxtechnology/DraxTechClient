
namespace Drax360Client
{
    public static class NetmanSafe
    {
        public static short GetInput(int EventNumber) => NetManUnsafe.GetInput(EventNumber);

        public static short GetNode(int EventNumber) => NetManUnsafe.GetNode(EventNumber);

        public static short GetNWMData(ref string FileName, short Index, ref StructuresUnsafe.DLLDATA LongArray, ref string DestString, ref string ExText, ref string ExText2) => NetManUnsafe.GetNWMData(ref FileName, Index, ref LongArray, ref DestString, ref ExText, ref ExText2);

        public static short GetNWMNumber(ref string FileName) => NetManUnsafe.GetNWMNumber(ref FileName);

        public static short GetOperatorNameText(int li, ref string s1) => NetManUnsafe.GetOperatorNameText(li, ref s1);

        public static short GetTimeString(int l1, ref string s1) => NetManUnsafe.GetTimeString(l1, ref s1);

        public static short GetZone(int EventNumber) => NetManUnsafe.GetZone(EventNumber);

        public static void InitialiseNWM(ref string Path) => NetManUnsafe.InitialiseNWM(ref Path);

        public static void LogMessageToAMX1(int EventType, int EventNumber, ref string Text, ref string TransferFile, int OpHandle) => NetManUnsafe.LogMessageToAMX1(EventType, EventNumber, ref Text, ref TransferFile, OpHandle);

        public static int MakeInputNumber(short Node, short Zone, short InputN, short InputType) => NetManUnsafe.MakeInputNumber(Node, Zone, InputN, InputType);

        public static void NwmForceEvmAttribute(ref string TransferFile, int EventNumber, short AttributeBit, short OnOff) => NetManUnsafe.NwmForceEvmAttribute(ref TransferFile, EventNumber, AttributeBit, OnOff);

        public static void SetNWMDDEchannel(short DDEChannel) => NetManUnsafe.SetNWMDDEchannel(DDEChannel);

        public static void SetupNWM(short giAmx1Offset) => NetManUnsafe.SetupNWM(giAmx1Offset);

        public static void WriteNWMData(ref string FileName, int EventType, int EventNumber, ref int LongArray, ref string TextParameter, ref string TextParameter2, ref string TextParameter3, short OnOff) => NetManUnsafe.WriteNWMData(ref FileName, EventType, EventNumber, ref LongArray, ref TextParameter, ref TextParameter2, ref TextParameter3, OnOff);

    }
}
