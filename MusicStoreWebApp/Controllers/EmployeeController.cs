using Microsoft.AspNetCore.Mvc;
using MusicStoreWebApp.Models;
using System.Data.SqlClient;
namespace MusicStoreWebApp.Controllers
{
    public class EmployeeController : Controller
    {
        private string conn_string = "Server=SHREYAS\\SQLEXPRESS;Database=MusicStore;Integrated Security=true;";
        private SqlConnection GetOpenConnection()
        {
            var conn = new SqlConnection(conn_string);
            conn.Open();
            return conn;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddSong()
        {
            return View(); // Make sure you have a corresponding View for adding songs
        }
        [HttpPost]
        public async Task<IActionResult> AddSong(SongViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Process the file here, for now, just store the file locally
                if (model.song_file != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", model.song_file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.song_file.CopyToAsync(stream);
                    }
                }

                // Save other song details to the database (not implemented here)

                return RedirectToAction("Index"); // Redirect to a suitable page after handling the file
            }
            return View(model); // Return the view with validation messages if any
        }

        public IActionResult ViewSongs()
        {
            List<SongViewModel> songs = new List<SongViewModel>();
            using (var connection = GetOpenConnection())
            {
                string query = "SELECT song_id, song_name, song_genre, song_album, song_artist, song_release_date, price FROM dbo.Songs";
                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        songs.Add(new SongViewModel
                        {
                            song_id = (long)reader["song_id"],
                            song_name = reader["song_name"].ToString(),
                            song_genre = reader["song_genre"].ToString(),
                            song_album = reader["song_album"].ToString(),
                            song_artist = reader["song_artist"].ToString(),
                            song_release_date = reader["song_release_date"] as DateTime?,
                            price = reader["price"] as float?
                        });
                    }
                }
            }
            return View(songs);
        }


        public IActionResult ViewCustomers()
        {
            List<CustomerViewModel> customers = new List<CustomerViewModel>();
            using (var connection = GetOpenConnection())
            {
                string query = "SELECT cust_id, cust_name, cust_email, cust_phone, cust_shipping_addr, cust_billing_addr FROM dbo.Customers";
                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(new CustomerViewModel
                        {
                            cust_id = (long)reader["cust_id"],
                            cust_name = reader["cust_name"].ToString(),
                            cust_email = reader["cust_email"].ToString(),
                            cust_phone = reader["cust_phone"].ToString(),
                            cust_shipping_addr = reader["cust_shipping_addr"].ToString(),
                            cust_billing_addr = reader["cust_billing_addr"].ToString(),
                        });
                    }
                }
            }
            return View(customers);
        }


        public IActionResult ViewStores()
        {
            List<StoreViewModel> stores = new List<StoreViewModel>();
            using (var connection = GetOpenConnection())
            {
                string query = "SELECT store_id, store_name, store_location FROM dbo.Stores";
                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        stores.Add(new StoreViewModel
                        {
                            store_id = (long)reader["store_id"],
                            store_name = reader["store_name"].ToString(),
                            store_location = reader["store_location"].ToString()
                        });
                    }
                }
            }
            return View(stores);
        }

    }
}
