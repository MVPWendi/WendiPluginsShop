
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using UnturnedWebForum.Extensions;

namespace Mvc.Client.Controllers
{
    public class HomeController : Controller
    {

        private readonly DBManager manager;

        public HomeController(DBManager manager)
        {
            this.manager = manager;
        }
        public IActionResult Index()
        {
            var plugins = manager.GetPlugins();
            ViewBag.Plugins = plugins;
            List<int> Counts = new();
            foreach (var plugin in plugins)
            {
                Counts.Add(manager.GetPurchasesCount(plugin.Name).Count);
            }
            ViewBag.Counts = Counts;
            return View();
            
        }

        public IActionResult HowToBuy()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        public IActionResult AdminPanel()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}