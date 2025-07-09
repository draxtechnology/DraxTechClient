using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drax360Client.AMXClean
{
    public class CSAMX
    {
        #region constants
        private const int kstartingfilenumber = 9000;
        #endregion

        #region private variables
        private int filenum = kstartingfilenumber;
        private string workingfolder = "";
        private List<NVM> nvms = new List<NVM>();
        #endregion


        #region constructor
        public CSAMX()
        {
            workingfolder = Path.GetDirectoryName(Application.ExecutablePath) + "//Temp//";
        }
        #endregion

        #region public methods
        public void LogMessage(int eventtype, int eventnumber, string text, int ophandle)
        {
            NVM ournvm = new NVM();
            ournvm.OurType = eventtype;
            ournvm.OurEvent = eventnumber;
            ournvm.Text = text;
            ournvm.Spare[0] = ophandle;
            nvms.Add(ournvm);
        }

        public void FlushMessages()
        {
            if (nvms.Count == 0) return;


            filenum++;
            string contents = "";
            foreach (NVM ournvm in nvms)
            {
                contents += ournvm.Render();
            }
            string filename = filenum.ToString() + ".GEN";
            string fullfilename = workingfolder + filename;

            File.WriteAllText(fullfilename, contents);
            if (filenum > 1000000) filenum = kstartingfilenumber;
            nvms.Clear();
        }
        #endregion
    }
}