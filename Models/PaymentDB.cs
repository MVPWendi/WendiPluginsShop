using System.ComponentModel.DataAnnotations;

namespace UnturnedWebForum.Models
{
    public class PaymentDB
    {
        public string UserName { get; set; }
        [Key]
        public string PaymentID { get; set; }
    }
}
