using System.Collections.Generic;

namespace UnturnedWebForum.Models.YooModels
{
    public class YooPayment
    {
        public string id { get; set; }
        public string status { get; set; }
        public YooPaymentAmount amount { get; set; }

        public YooPaymentAmount income_amount { get; set; }
        public string description { get; set; }

        public YooRecipient recipient { get; set; }
        public YooPaymentMethod payment_method { get; set; }

        public string captured_at { get; set; }

        public string created_at { get; set; }
        public string expires_at { get; set; }
        public YooConfirmation confirmation { get; set; }

        public bool test { get; set; }
     //   public YooRefundedAmount refunded_amount { get; set; }

        public bool paid { get; set; }
        public bool refundable { get; set; }

        public string receipt_registration { get; set; }
        public Dictionary<string, string> metadata { get; set; }
       // public YooCancellationDetails cancellation_details { get; set; }
       // public YooAuthoDetails authorization_details {get;set;}

      //  public List<YooTransfer> transfers { get; set; }
        public YooDeal deal { get; set; }
        public string merchant_customer_id { get; set; }
    }
     public class YooDeal
    {
        public string id { get; set; }
        public List<YooSettle> settlements { get; set; }
    }
    public class YooConfirmation
    {
        public string type { get; set; }
        public string confirmation_url { get; set; }
    }
    public class YooSettle
    {
        public string type { get; set; }
        public YooPaymentAmount amount { get; set; }
    }
    public class YooPaymentMethod
    {
        public string type { get; set; }
        public string id { get; set; }
        public bool saved { get; set; }
        public string title { get; set; }
    }
    public class YooPaymentAmount
    {
        public string value { get; set; }
        public string currency { get; set; }
    }

    public class YooRecipient
    {
        public string account_id { get; set; }
        public string gatewat_id { get; set; }
    }


    public class YooSendPayment
    {
        public YooPaymentAmount amount { get; set; }
        public string description { get; set; }
        public YooRecipient receipt { get; set; }
        public YooRecip receipent { get; set; }

        public string payment_token { get; set; }
        public string payment_method_id { get; set; }
        public object payment_method_data { get; set; }

        public bool capture { get; set; }
        public YooConfirmation confirmation { get; set; }

        
    }
    public class YooRecip
    {
        public string gateway_id { get; set; }
    }
}
