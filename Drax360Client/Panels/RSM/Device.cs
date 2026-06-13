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
            ID = Guid.NewGuid();
            Name = string.Empty;
            IP = string.Empty;
            Site = string.Empty;
        }

        /// <summary>
        ///  going to use this as the unique identifier for devices, so we can track them even if the name or IP changes. It's a simple solution that avoids issues with duplicate names or IPs.
        /// </summary>
        public Guid ID { get; set; }

        public string Name { get; set; }
        public string IP { get; set; }

        // User-entered site label, shown in the RSM node grid's "Site Name"
        // column. Like Name, it's a manual label (not panel-reported) and is
        // persisted to devices.json. Node Name maps to Name above.
        public string Site { get; set; }
    }
}