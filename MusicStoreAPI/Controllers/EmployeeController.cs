using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MusicStoreWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStoreWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private string conn_string = "Server=SHREYAS\\SQLEXPRESS;Database=MusicStore;Integrated Security=true;Encrypt=True;TrustServerCertificate=True;";

        private SqlConnection GetOpenConnection()
        {
            var conn = new SqlConnection(conn_string);
            conn.Open();
            return conn;
        }

        // GET: api/EmployeeApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeViewModel>>> GetAllEmployees()
        {
            List<EmployeeViewModel> employees = new List<EmployeeViewModel>();
            using (var conn = GetOpenConnection())
            {
                string query = "SELECT emp_id, emp_name, emp_email, emp_phone, emp_addr, user_type, emp_store_id FROM dbo.Employees";
                var cmd = new SqlCommand(query, conn);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        employees.Add(new EmployeeViewModel
                        {
                            emp_id = reader.GetInt64(0),
                            emp_name = reader.IsDBNull(1) ? null : reader.GetString(1),
                            emp_email = reader.IsDBNull(2) ? null : reader.GetString(2),
                            emp_phone = reader.IsDBNull(3) ? null : reader.GetString(3),
                            emp_addr = reader.IsDBNull(4) ? null : reader.GetString(4),
                            user_type = reader.IsDBNull(5) ? null : reader.GetString(5),
                            emp_store_id = reader.IsDBNull(6) ? 0 : reader.GetInt64(6)
                        });
                    }
                }
            }

            return Ok(employees);
        }
    }
}
