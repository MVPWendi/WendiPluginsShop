/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/aspnet-contrib/AspNet.Security.OpenId.Providers
 * for more information concerning the license and the contributors participating to this project.
 */

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SteamTest.Models;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UnturnedWebForum.Extensions;
using Microsoft.AspNetCore.Http;
using UnturnedWebForum.Models.YooModels;
using RestSharp;
using RestSharp.Authenticators;
using UnturnedWebForum.Models.Payments;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Mvc.Client.Controllers
{
    public class ProfileController : Controller
    {
        private readonly DBManager manager;

        public ProfileController(DBManager manager)
        {
            this.manager = manager;

        }
        public ActionResult DownloadDocument()
        {
            string filePath = "wwwroot\\WendiLoader.dll";
            string fileName = "WendiLoader.dll";

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "application/force-download", fileName);

        }
        [Authorize()]
        [HttpGet]
        public IActionResult UserProfile()
        {
            User ForumUser = manager.GetUser(User.Identity.Name);
            if (ForumUser == null)
            {
                return Content("User null");
            }
            ViewBag.User = ForumUser;
            return View();
        }
        [Authorize()]
        [HttpGet]
        public IActionResult UserPlugins()
        {
            User ForumUser = manager.GetUser(User.Identity.Name);

            var Purchases = manager.GetPurchasesByKey(ForumUser.LicenseKey);
            ViewBag.Plugins = Purchases;
            if (ForumUser == null)
            {
                return Content("User null");
            }

            ViewBag.User = ForumUser;
            return View();
        }
        [Authorize()]
        [HttpGet]
        public IActionResult Loader()
        {
            User ForumUser = manager.GetUser(User.Identity.Name);
            if (ForumUser == null)
            {
                return Content("User null");
            }
            ViewBag.User = ForumUser;
            return View();
        }
        [Authorize()]
        [HttpGet]
        public IActionResult AddBalance()
        {
            User ForumUser = manager.GetUser(User.Identity.Name);
            if (ForumUser == null)
            {
                return Content("User null");
            }
            ViewBag.User = ForumUser;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateBalance()
        {

            var payments = manager.GetPaymentForUser(User.Identity.Name);
            foreach(var payment in payments)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://api.yookassa.ru/v3/payments/"+payment.PaymentID))
                    {

                        var username = "864674";
                        var password = "live__MD9pBAXeuyKoOs4cB7x_rjebSSM3lua3ITbJPKRJio";
                        string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                                       .GetBytes(username + ":" + password));

                        request.Headers.TryAddWithoutValidation("Authorization", $"Basic {encoded}");

                        var response = await httpClient.SendAsync(request);
                        var json = JsonSerializer.Deserialize<YooPayment>(await response.Content.ReadAsStringAsync());
                        if(json.paid)
                        {
                            var amount = Convert.ToDecimal(json.amount.value.Replace('.', ','));
                            manager.ChangeBalance(User.Identity.Name, Convert.ToDecimal(amount));
                            manager.RemovePayment(payment.PaymentID);
                        }

                       
                    }
                }
            }
            return Redirect("https://wendiplugins.ru/Profile/UserProfile");
        }
        [Authorize()]
        
        public async Task<ActionResult> AddBalance(int amount)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.yookassa.ru/v3/payments"))
                {
                    request.Headers.TryAddWithoutValidation("Idempotence-Key", Guid.NewGuid().ToString());
                    var username = "864674";
                    var password = "live__MD9pBAXeuyKoOs4cB7x_rjebSSM3lua3ITbJPKRJio";
                    string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                                   .GetBytes(username + ":" + password));

                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {encoded}");
                    var data = new System.Collections.Generic.Dictionary<string, string>();
                    data.Add("UserName", User.Identity.Name);
                    var payment = new Payment
                    {
                        amount = new AmountObject { value = amount.ToString(), currency = "RUB" },
                        confirmation = new ConfirmationObject { return_url = "https://wendiplugins.ru/Profile/UpdateBalance", type = "redirect" },
                        description = "Пополнение баланса для пользователя: " + User.Identity.Name + " на сумму " + amount.ToString() + "RUB",
                        capture = true,
                        metadata = data
                        
                    };

                    request.Content = new StringContent(JsonSerializer.Serialize(payment));
                        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    var json = JsonSerializer.Deserialize<YooPayment>(await response.Content.ReadAsStringAsync());
                    try
                    {
                        manager.AddPayment(User.Identity.Name, json.id);
                    }
                    catch(Exception ex)
                    {
                        Redirect(ex.InnerException.Message);
                    }
                    return Redirect(json.confirmation.confirmation_url);
                }
            }
        }
        
       
    }
}