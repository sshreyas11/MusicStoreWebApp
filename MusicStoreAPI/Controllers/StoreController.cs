using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MusicStoreWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStoreWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoreApiController : ControllerBase
    {
        private string conn_string = "Server=SHREYAS\\SQLEXPRESS;Database=MusicStore;Integrated Security=true;Encrypt=True;TrustServerCertificate=True;";

        private SqlConnection GetOpenConnection()
        {
            var conn = new SqlConnection(conn_string);
            conn.Open();
            return conn;
        }

        // GET: api/StoreApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreViewModel>>> GetAllStores()
        {
            List<StoreViewModel> stores = new List<StoreViewModel>();
            using (var conn = GetOpenConnection())
            {
                string query = "SELECT store_id, store_name, store_location FROM dbo.Stores";
                var cmd = new SqlCommand(query, conn);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        stores.Add(new StoreViewModel
                        {
                            store_id = reader.GetInt64(0),
                            store_name = reader.IsDBNull(1) ? null : reader.GetString(1),
                            store_location = reader.IsDBNull(2) ? null : reader.GetString(2)
                        });
                    }
                }
            }

            return Ok(stores);
        }
    }
}
