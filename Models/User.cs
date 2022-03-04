using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SteamTest.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Password { get; set; }
        
        public string UserName { get; set; }
        public string Email { get; set; }

        public string LicenseKey { get; set; }
        public int? RoleId { get; set; }

        public decimal MoneyAmount { get; set; }
        public Role Role { get; set; }
    }
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Role()
        {
            
        }
    }
}
