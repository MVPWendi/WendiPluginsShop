using Microsoft.AspNetCore.Mvc;
using SteamTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnturnedWebForum.Extensions;
using UnturnedWebForum.Models;

namespace UnturnedWebForum.Controllers
{
    public class PluginController : Controller
    {
        private readonly DBManager manager;

        public PluginController(DBManager manager)
        {
            this.manager = manager;
            
        }

        public IActionResult PluginInfo(string id)
        {
            Plugin plugin = manager.GetPluginByName(id);
            ViewBag.Plugin = plugin;
            return View();
        }

        
       
        public IActionResult Buy(string id)
        {
            ViewBag.PluginName = id;
            ViewBag.UserName = User.Identity.Name;
            return View();
        }
        [HttpPost]
        public IActionResult Buy(string IP, string Port, string PluginName)
        {
            Plugin plugin = manager.GetPluginByName(PluginName);
            User user = manager.GetUser(User.Identity.Name);
            if (plugin == null || user == null || user.MoneyAmount < Convert.ToInt32(plugin.Cost))
            {
                return RedirectToAction("Index", "Home");
            }
            user.MoneyAmount -= Convert.ToInt32(plugin.Cost);
            manager.AddPurchase(user, IP, Port, PluginName); //tyt method

            return RedirectToAction("UserPlugins", "Profile");
        }
    }
}
