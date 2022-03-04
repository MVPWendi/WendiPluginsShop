using System.Collections.Generic;

namespace UnturnedWebForum.Models.Payments
{
    public class Payment
    {
        public AmountObject amount { get; set; }
        public ConfirmationObject confirmation { get; set; }

        public bool capture { get; set; }
        public string description { get; set; }

        public Dictionary<string, string> metadata { get; set; }
    }

    public class ConfirmationObject
    {
        public string type { get; set; }
        public string return_url { get; set; }
    }
    public class AmountObject
    {
        public string value { get; set; }
        public string currency { get; set; }
    }

}
