using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MusicStoreWebApp.Models;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace MusicStoreWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeApiController : ControllerBase
    {
        private string conn_string = "Server=SHREYAS\\SQLEXPRESS;Database=MusicStore;Integrated Security=true;";

        private SqlConnection GetOpenConnection()
        {
            var conn = new SqlConnection(conn_string);
            conn.Open();
            return conn;
        }

        // POST: api/EmployeeApi/Register
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterEmployee([FromBody] EmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var phone = model.emp_phone;
            if (!phone.StartsWith("+1"))
            {
                phone = "+1 " + phone;
            }

            using (var conn = GetOpenConnection())
            {
                var query = "INSERT INTO Employees (emp_id, emp_name, emp_email, emp_phone, emp_addr, user_type, emp_store_id) " +
                           "VALUES (@emp_id, @emp_name, @emp_email, @emp_phone, @emp_addr, @user_type, @emp_store_id)";
                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@emp_id", model.emp_id);
                cmd.Parameters.AddWithValue("@emp_name", model.emp_name);
                cmd.Parameters.AddWithValue("@emp_email", model.emp_email);
                cmd.Parameters.AddWithValue("@emp_phone", phone);
                cmd.Parameters.AddWithValue("@emp_addr", model.emp_addr);
                cmd.Parameters.AddWithValue("@user_type", model.user_type);
                cmd.Parameters.AddWithValue("@emp_store_id", model.emp_store_id);
                await cmd.ExecuteNonQueryAsync();
            }

            return CreatedAtAction(nameof(GetEmployee), new { id = model.emp_id }, model);
        }

        // GET: api/EmployeeApi/5
        [HttpGet("{id}")]
        public IActionResult GetEmployee(long id)
        {
            using (var conn = GetOpenConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Employees WHERE emp_id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    var employee = new EmployeeViewModel
                    {
                        emp_id = Convert.ToInt64(reader["emp_id"]),
                        emp_name = reader["emp_name"].ToString(),
                        emp_email = reader["emp_email"].ToString(),
                        emp_phone = reader["emp_phone"].ToString(),
                        emp_addr = reader["emp_addr"].ToString(),
                        user_type = reader["user_type"].ToString(),
                        emp_store_id = Convert.ToInt64(reader["emp_store_id"])
                    };
                    return Ok(employee);
                }
            }
            return NotFound();
        }
    }
}
