using APIs.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
namespace APIs.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class User: ControllerBase {
        private string conn_string = "Server=U1-8C9BGY3-L\\SQLEXPRESS;Database=MusicStore;Integrated Security=true;";

        private SqlConnection GetOpenConnection() {
            var conn = new SqlConnection(conn_string);
            conn.Open();
            return conn;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> registerUser([FromBody] UserModel user) {


            var phone = user.usr_pho;
            if (!phone.StartsWith("+1")) {
                phone = "+1" + phone;
            }
            using (var conn = GetOpenConnection()) {
                var query = "INSERT INTO Users (first_name, last_name, email, password_hash, usr_pho)" +
                    "VALUES (@first_name, @last_name, @email, @password_hash, @usr_pho)";

                var cmd = new SqlCommand(query, conn);
                
                cmd.Parameters.AddWithValue("@first_name", user.first_name);
                cmd.Parameters.AddWithValue("@last_name", user.last_name);
                cmd.Parameters.AddWithValue("@email", user.email);
                cmd.Parameters.AddWithValue("@password_hash", user.password_hash);
                cmd.Parameters.AddWithValue("@usr_pho", user.usr_pho);

                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                if (rowsAffected > 0) {
                    return Ok(new { message = "User added successfully" });
                } else {
                    return StatusCode(500, "Error while adding User");
                }
            }

        }

        [HttpGet("{id}")]
        public UserModel getUser(long id) {
            UserModel model = null;
            using (var conn = GetOpenConnection()) {
                string query = "SELECT * from Users WHERE user_id=@id;";
                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                using (var cReader = cmd.ExecuteReader()) {
                    if (cReader.Read()) {
                        model = new UserModel {
                            user_id = Convert.ToInt64(cReader[0]),
                            user_type = Convert.ToString(cReader[1]),
                            first_name = Convert.ToString(cReader[2]),
                            last_name = Convert.ToString(cReader[3]),
                            email = Convert.ToString(cReader[4]),
                            usr_pho = Convert.ToString(cReader[5]),
                        };
                    }
                }
            }
            return model;
        }

        [HttpGet("customers")]
        public List<UserModel> viewCustomers() {
            var customers = new List<UserModel>();
            using (var conn = GetOpenConnection()) {
                var cmd = new SqlCommand("SELECT * FROM Users WHERE user_type = @user_type", conn);
                cmd.Parameters.AddWithValue("@user_type", "C");
                var cReader = cmd.ExecuteReader();
                
                while (cReader.Read()) {
                    UserModel model = new UserModel {
                        user_id = Convert.ToInt64(cReader[0]),
                        user_type = Convert.ToString(cReader[1]),
                        first_name = Convert.ToString(cReader[2]),
                        last_name = Convert.ToString(cReader[3]),
                        email = Convert.ToString(cReader[4]),
                        usr_pho = Convert.ToString(cReader[5]),
                    };
                    customers.Add(model);
                }
            }
            return customers;
        }

        [HttpGet("employees")]
        public List<UserModel> viewEmployees() {
            var employees = new List<UserModel>();
            using (var conn = GetOpenConnection()) {
                var cmd = new SqlCommand("SELECT * FROM Users WHERE user_type = @user_type", conn);
                cmd.Parameters.AddWithValue("@user_type", "E");
                var cReader = cmd.ExecuteReader();

                while (cReader.Read()) {
                    UserModel model = new UserModel {
                        user_id = Convert.ToInt64(cReader[0]),
                        user_type = Convert.ToString(cReader[1]),
                        first_name = Convert.ToString(cReader[2]),
                        last_name = Convert.ToString(cReader[3]),
                        email = Convert.ToString(cReader[4]),
                        usr_pho = Convert.ToString(cReader[5]),
                    };

                    employees.Add(model);
                }
            }
            return employees;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model){
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            using (var conn = GetOpenConnection()){
                var query = "SELECT first_name FROM Users WHERE email = @cust_email AND password_hash = @cust_password";
                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@cust_email", model.cust_email);
                cmd.Parameters.AddWithValue("@cust_password", model.cust_password); // Ensure passwords are hashed and compared securely
                var reader = await cmd.ExecuteReaderAsync();
        
                if (reader.Read()){
                    var firstName = reader["first_name"].ToString();
                    return Ok(new { message = "Login successful", first_name = firstName });
                }
            }
            return Unauthorized(new { message = "Invalid email or password" });
        }
    }
}
