using System.ComponentModel.DataAnnotations;
namespace APIs.Models {
    public class UserModel {

        [Key]
        public Int64 user_id { get; set; }
        public string user_type { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string password_hash { get; set; }
        public string usr_pho {  get; set; }
    }
}
