using System.Collections.Generic;

namespace UnturnedWebForum.Models
{
    public class LoaderCheckModel
    {
        public List<AssData> Plugins { get; set; }
        public string Port { get; set; }
        public string LicenseKey { get; set; }

        public LoaderCheckModel(List<AssData> Plugins, string Port, string LicenseKey)
        {
            this.Plugins = Plugins;
            this.Port = Port;
            this.LicenseKey = LicenseKey;

        }

    }
    public class AssData
    {
        public byte[] pluginbytes { get; set; }
        public string pluginname { get; set; }
    }
}
