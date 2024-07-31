namespace APIs.Models {
  public class LoginViewModel
    {
        [Required]
        public string cust_username { get; set; }

        [Required]
        public string cust_password { get; set; }
    }
}
