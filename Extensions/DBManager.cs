using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SteamTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnturnedWebForum.Models;

namespace UnturnedWebForum.Extensions
{
    public  class DBManager
    {
        private readonly UserContext db;
        public DBManager(UserContext context)
        {
            db = context;
        }

        public List<Purchase> GetPurchasesByKey(string key)
        {
                var purchases = db.Purchases.Where(p => p.LicenseKey == key).ToList();
                return purchases;                       
        }
        public List<Purchase> GetPurchasesByKey(string key, string ip, string port)
        {
            var purchases = db.Purchases.Where(p => p.LicenseKey == key && p.ServerIp == ip && p.ServerPort == port).ToList();
            return purchases;
        }

        public List<PaymentDB> GetPaymentForUser(string username)
        {
            return (from x in db.Payments where x.UserName == username select x).ToList();
        }


        public void ChangeBalance(string username, decimal money)
        {
            var user = GetUser(username);
            user.MoneyAmount += money;
            db.SaveChanges();
        }

        public void RemovePayment(string paymentid)
        {
            var payment = db.Payments.FirstOrDefault(x => x.PaymentID == paymentid);
            db.Payments.Remove(payment);
            db.SaveChanges();
        }
        public void AddPayment(string username, string id)
        {
            db.Payments.Add(new PaymentDB { PaymentID = id, UserName = username });
            db.SaveChanges();
        }
        public  bool IsKeyExist(string key, string ip, string port)
        {
            var purchases = db.Purchases.Where(p => p.LicenseKey == key && p.ServerIp == ip && p.ServerPort == port).ToList();
            if (purchases.Any()) return true;
            return false;
        }
        public List<Plugin> GetPlugins()
        {
            return db.Plugins.ToList();
        }

        public void AddPurchase(User user, string IP, string Port, string PluginName)
        {
            var plugin = GetPluginByName(PluginName);
            db.Purchases.Add(new Purchase
            {
                Cost = plugin.Cost,
                Date = DateTime.Now,
                LicenseKey = user.LicenseKey,
                PluginName = PluginName,
                ServerIp = IP,
                ServerPort = Port,
                UserId = user.Id
            });
            db.SaveChanges();
        }

        public  Plugin GetPluginByName(string id)
        {
                var plugin = db.Plugins.FirstOrDefault(p => p.Name == id);
                return plugin;

        }

        public User GetUser(string name)
        {
            return  db.Users.FirstOrDefaultAsync(p => p.UserName == name).Result;
        }
        public User GetUser(int id)
        {
           return db.Users.FirstOrDefaultAsync(p => p.Id == id).Result;
        }
        public List<Purchase> GetPurchasesCount(string pluginname)
        {
            var purchases = from Purchase in db.Purchases where Purchase.PluginName == pluginname orderby Purchase.Date select Purchase;
            return purchases.ToListAsync().Result;
        }
    }
}
