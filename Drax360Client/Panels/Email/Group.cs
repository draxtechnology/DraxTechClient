using System.Collections.Generic;

namespace DraxClient.Panels.Email
{
    public class Group
    {
        public Group()
        {
            Addresses = new List<Address>();
            Name = string.Empty;
            Enabled = true;
            Description = string.Empty;
            Keywords = string.Empty;
            LocText = false;
            NodeText = false;
            HTML = false;
            BCC = false;
            Reports = false;
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public bool Enabled { get; set; }

        public string Keywords { get; set; }

        // Ensure this is public and initialized so it will be serialized.
        public List<Address> Addresses { get; set; }

        // Flags used by the form
        public bool LocText { get; set; }
        public bool NodeText { get; set; }
        public bool HTML { get; set; }
        public bool BCC { get; set; }
        public bool Reports { get; set; }
    }
}