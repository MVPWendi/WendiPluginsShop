using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UnturnedWebForum.Models
{
    public class Purchase
    {
        [Key]
        public int Id { get; set; }
        public string PluginName { get; set; }
        public int UserId { get; set; }
        public string ServerIp { get; set; }
        public string ServerPort { get; set; }
        public DateTime Date { get; set; }
        public decimal Cost { get; set; }
        public string LicenseKey { get; set; }
    }
}
