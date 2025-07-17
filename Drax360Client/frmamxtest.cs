using Drax360Client.AMXClean;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static Drax360Client.StructuresSafe;

namespace Drax360Client
{
    public partial class frmamxtest : Form
    {
        private CSAMX cleanamx = new CSAMX();
        
        public frmamxtest()
        {
            InitializeComponent();


            string tempRefParam = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\";
            NetmanSafe.InitialiseNWM(ref tempRefParam);

            // not sure if this is needed
            int amxmainoffset = 1; // light AMX,  other values indicate full version;
            NetmanSafe.SetupNWM((short)amxmainoffset);


            //GENNetManager.DDEChannel = Convert.ToInt32(Conversion.Val(ReadFromIniFile("DDEChannel", HandleOfThisNWM.ToString(), "-1", MycroftVBDefs.CURRENTNWMDATAFILE)));

            NetmanSafe.SetNWMDDEchannel((short)99);
        }

        private static string transferfilename = "";
        private static int transferid = 0;

        public static int NWMBufInPtr = 0; //Buffer input pointer where next command should be put
        public static int NWMBufOutPtr = 0; //Buffer output pointer to current command for transfer
        public static string[] NWMBuffer = new string[64];


        internal static string GetCurrentTransferFile()
        {

            if (transferfilename == "")
            {
                transferfilename = MakeTransferFilename();
            }
            return transferfilename;

        }

        internal static string MakeTransferFilename()
        {

            string result = "";
            transferid++; //Next filenumber

            result = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\Temp\\{transferid.ToString()}.GEN";
            if (transferid > 1000000)
            {
                transferid = 0;
            }

            return result;
        }


        internal static void QueueTransferData(string TransferData)
        {

            //UPGRADE_TODO: (1069) Error handling statement (On Error Resume Next) was converted to a pattern that might have a different behavior. More Information: https://docs.mobilize.net/vbuc/ewis/todos#id-1069
            try
            {
                NWMBuffer[NWMBufInPtr] = TransferData;
                NWMBufInPtr++; //Point to next
                if (NWMBufInPtr >= 64) // mytMaxNWMBuffers
                {
                    NWMBufInPtr = 0; //Cycle back to the beginning
                }
            }
            catch (Exception exc)
            {

            }

        }

        internal static void FlushAMX1Messages()
        {

            //Traps
            if (transferfilename == "")
            {
                return;
            }
            if (FileSystem.Dir(transferfilename) == "") return;
            if (((int)(new FileInfo(transferfilename)).Length) == 0) return;

            if (GetQueuedData() > (64 - 8)) // 64 = MycroftVBDefs.mytMaxNWMBuffers;
            {
                return;
            }
            //Now queue it
            QueueTransferData($"NTX:{transferfilename}");
            //test change transferData
            //_messageSender.SendMessage($"NTX:{transferfilename}");
            transferfilename = ""; //Start a new file next time a transfer is needed

        }



        internal static int GetQueuedData()
        {

            int result = 0;
            if (NWMBufInPtr == NWMBufOutPtr)
            {
                result = 0;
            }
            else
            {
                result = NWMBufInPtr - NWMBufOutPtr;
                if (result < 1)
                {
                    result += 64; // MycroftVBDefs.mytMaxNWMBuffers;
                }
            }

            return result;
        }

        private void btnfakeisolation_Click(object sender, EventArgs e)
        {
            {

                int Node = 0;
                int Ipt = 0, Zone = 0, InputType = 0;
                int amxoffset = 1; // 0 amxlight
                bool OnOff = false, Isolation = false;
                int evnum = 0;
                StructuresUnsafe.DLLDATA DLL = StructuresUnsafe.DLLDATA.CreateInstance();

                int NumIsols = 0; //Count how many there are
                string ps = ""; //Default for text parameter
                string tStr = GetCurrentTransferFile();

                for (int n = 1; n <= 2; n++)
                //for (int n = 1; n <= 24; n++)
                {
                    Node = 1;
                    Zone = 2;
                    Ipt = 1;
                    InputType = 1;
                    OnOff = true;
                    Isolation = true;
                    if (Node != 0)
                    {
                        //Now send a fake isolation signal to log and isolation list
                        if (Isolation)
                        {
                            evnum = NetmanSafe.MakeInputNumber((short)(Node + amxoffset), (short)Zone, (short)Ipt, 0);
                            //                Call WriteNWMData(tStr, 2, evNum, DLL.Dat(0), ps, "", frmEIPNodes.GetNodeTextByNode(Node), CInt(OnOff))    'Signal isolation
                            string tempRefParam = "";
                            string tempRefParam2 = "";
                            NetmanSafe.WriteNWMData(ref tStr, 2, evnum, ref DLL.Dat[0], ref ps, ref tempRefParam, ref tempRefParam2, (short)((OnOff) ? -1 : 0)); //Signal isolation
                        }
                        //Fake an "input" from the remote
                        evnum = NetmanSafe.MakeInputNumber((short)(Node + amxoffset), (short)Zone, (short)Ipt, (short)InputType);
                        if (OnOff)
                        {
                            evnum += unchecked((int)0x80000000);
                        }
                        //            Call WriteNWMData(tStr, 1, evNum, DLL.Dat(0), ps, "", frmEIPNodes.GetNodeTextByNode(Node), CInt(OnOff))    'Signal alarm
                        string tempRefParam3 = "";
                        string tempRefParam4 = "";
                        NetmanSafe.WriteNWMData(ref tStr, 1, evnum, ref DLL.Dat[0], ref ps, ref tempRefParam3, ref tempRefParam4, (short)((OnOff) ? -1 : 0)); //Signal alarm
                        NumIsols++;
                    }
                    else
                    {
                        break;
                    }

                }

                if (NumIsols != 0)
                {
                    //Only queue data when some isols were found
                    FlushAMX1Messages();
                }

            }

        }

        private void btntestmessage_Click(object sender, EventArgs e)
        {
            string tempRefParam = "c# using Gen netman GEN NWM Started...15:29";
            string tempRefParam2 = GetCurrentTransferFile();

            // this works 
            File.WriteAllText(tempRefParam2, tempRefParam);


            NetmanSafe.LogMessageToAMX1(9, 0, ref tempRefParam, ref tempRefParam2, 3);
            FlushAMX1Messages();
        }

        private void btgettime_Click(object sender, EventArgs e)
        {

            int LongTime = 99;
            string st = new String(' ', 24);
            short zz = NetmanSafe.GetTimeString(LongTime, ref st);

        }

        private void bttestmessageclean_Click(object sender, EventArgs e)
        {
            string strmsg = "c# Gent Started " + DateTime.Now.ToString(); 
            cleanamx.LogMessage(9, 0, strmsg, 3);
            cleanamx.FlushMessages();

            string tempRefParam2 = GetCurrentTransferFile();
            NetmanSafe.LogMessageToAMX1(9, 0, ref strmsg, ref tempRefParam2, 3);
            FlushAMX1Messages();
        }

        private void btcleanisoloate_Click(object sender, EventArgs e)
        {
            int Node = 0;
            int Ipt = 0, Zone = 0, InputType = 0;
            int amxoffset = 1; // 0 amxlight
            bool OnOff = false, Isolation = false;
            int evnum = 0;


            int numberofisolations = 0; //Count how many there are
            string ps = ""; //Default for text parameter


            for (int n = 1; n <= 2; n++)
            //for (int n = 1; n <= 24; n++)
            {
                Node = 1;
                Zone = 2;
                Ipt = 1;
                InputType = 1;
                OnOff = true;
                Isolation = true;
                if (Node != 0)
                {
                    //Now send a fake isolation signal to log and isolation list
                    if (Isolation)
                    {


                        evnum = cleanamx.MakeInputNumber(Node + amxoffset, Zone, Ipt, 0);

                        string tempRefParam = "";
                        string tempRefParam2 = "";
                        cleanamx.WriteData(2, evnum, ps, tempRefParam, tempRefParam2, OnOff);

                    }
                    //Fake an "input" from the remote
                    evnum = cleanamx.MakeInputNumber(Node + amxoffset, Zone, Ipt, InputType);
                    if (OnOff)
                    {
                        evnum += cleanamx.IncrementInputNumber(evnum);
                    }

                    string tempRefParam3 = "";
                    string tempRefParam4 = "";
                    cleanamx.WriteData(1, evnum, ps, tempRefParam3, tempRefParam4, OnOff);
                    numberofisolations++;
                }
                else
                {
                    break;
                }

            }

            if (numberofisolations != 0)
            {
                //Only queue data when some isols were found
                cleanamx.FlushMessages();
            }
        }

        private void btcleanalarm_Click(object sender, EventArgs e)
        {
            alarm(true);
        }

    
        private void btcleanalarmoff_Click(object sender, EventArgs e)
        {
            alarm(false);
            
        }

        private void alarm(bool on)
        {
            int amxoffset = 1; // 0 amxlight
            int node = 1;
            int zone = 2;
            int inputtype = 1;

            int evnum = cleanamx.MakeInputNumber(node + amxoffset, zone, inputtype, 0);



            string ps = "##TEST";
            string tempRefParam = "";
            string tempRefParam2 = "";
            cleanamx.WriteData(1, evnum, ps, tempRefParam, tempRefParam2, on);
            cleanamx.FlushMessages();
        }

    }
}