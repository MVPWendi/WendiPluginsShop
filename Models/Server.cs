using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UnturnedWebForum.Models
{
    public class Server
    {

        public string IP { get; set; }
        public string Port { get; set; }
        [Key]
        public int ID { get; set; }
    }
}
