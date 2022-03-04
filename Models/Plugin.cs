using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UnturnedWebForum.Models
{
    public class Plugin
    {

        public string Path { get; set; }
        [Key]
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }

        public bool HaveWorkshop { get; set; }
        public decimal Cost { get; set; }
    }
}
