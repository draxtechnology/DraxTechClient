
using System.Runtime.InteropServices;


namespace Drax360Client
{
    public static class StructuresUnsafe
    {

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct DLLDATA
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 33)]
            public int[] Dat;
            private static void InitStruct(ref DLLDATA result, bool init) => result.Dat = new int[33];

            public static DLLDATA CreateInstance()
            {
                DLLDATA result = new DLLDATA();
                InitStruct(ref result, true);
                return result;
            }
            public DLLDATA Clone()
            {
                DLLDATA result = this;
                InitStruct(ref result, false);
                Array.Copy(this.Dat, result.Dat, 33);
                return result;
            }
        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            public short wYear;
            public short wMonth;
            public short wDayOfWeek;
            public short wDay;
            public short wHour;
            public short wMinute;
            public short wSecond;
            public short wMilliseconds;
        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct TIME_ZONE_INFORMATION
        {
            public int Bias;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public short[] StandardName;
            public SYSTEMTIME StandardDate;
            public int StandardBias;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public short[] DaylightName;
            public SYSTEMTIME DaylightDate;
            public int DaylightBias;
        }
    }
}
