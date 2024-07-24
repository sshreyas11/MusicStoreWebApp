using System.ComponentModel.DataAnnotations;

namespace MusicStoreWebApp.Models
{
    public class CustomerViewModel
    {
        [Required]
        public Int64 cust_id { get; set; }

        [Required]
        public string cust_email { get; set; }

        [Required]
        public string cust_name { get; set; }

        [Required]
        [RegularExpression(@"\d{10}", ErrorMessage = "Please enter a valid 10-digit phone number")]
        public string cust_phone { get; set; }

        [Required]
        public string cust_shipping_addr { get; set; }

        [Required]
        public Int64 preferred_store_id { get; set; }

        [Required]
        public string cust_username { get; set; }

        [Required]
        public string cust_billing_addr { get; set; }
        
        
    }
}
