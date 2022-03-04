
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;
using UnturnedWebForum.Extensions;
using UnturnedWebForum.Models;


namespace UnturnedWebForum.Controllers
{
    public class apiController : Controller
    {
        private readonly DBManager manager;

        public apiController(DBManager manager)
        {
            this.manager = manager;
            
        }

        public string Check(string model)
        {
            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
            
            LoaderCheckModel model1 = JsonSerializer.Deserialize<LoaderCheckModel>(model);

            if (model1 == null) return "Error";
            string key = model1.LicenseKey;
            string port = model1.Port;
            var WanterdPlugins = model1.Plugins;
            if (manager.IsKeyExist(key, remoteIpAddress.ToString(), port))
            {
                
                var Purchases = manager.GetPurchasesByKey(key, remoteIpAddress.ToString(), port);

                foreach (var purchase in Purchases)
                {
                    var plugin = manager.GetPluginByName(purchase.PluginName);
                    var wantedplugin = WanterdPlugins.Find(x => x.pluginname == plugin.Name);
                    if (wantedplugin!=null)
                    {
                        wantedplugin.pluginbytes = System.IO.File.ReadAllBytes("wwwroot\\Plugins\\"+plugin.Path+".dll");
                    }
                }
               
            }
            return JsonSerializer.Serialize(model1);

        }

    }
}
