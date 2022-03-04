using System.Collections.Generic;

namespace UnturnedWebForum.Models
{
    public class PluginsAnswerModel
    {
        public List<PluginsAnswer> Plugins { get; set; }

        public PluginsAnswerModel(List<PluginsAnswer> Plugins)
        {
            this.Plugins = Plugins;
        }
    }


    public class PluginsAnswer
    {
        public byte[] PluginByte { get; set; }
        public string PluginName { get; set; }


        public PluginsAnswer(byte[] PluginByte, string PluginName)
        {
            this.PluginByte = PluginByte;
            this.PluginName = PluginName;
        }

    }
}
