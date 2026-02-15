using DraxClient.Panels.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraxClient.Panels.RSM
{
    public class Device
    {

        public Device()
        {
            ID = new Guid();
            Name = string.Empty;
            IP = string.Empty;
        }

        /// <summary>
        ///  going to use this as the unique identifier for devices, so we can track them even if the name or IP changes. It's a simple solution that avoids issues with duplicate names or IPs.
        /// </summary>
        public Guid ID { get; set; }

        public string Name { get; set; }
        public string IP { get; set; }
    }
}