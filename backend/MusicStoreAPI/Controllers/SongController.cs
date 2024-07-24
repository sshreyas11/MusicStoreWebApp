using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MusicStoreWebApp.Models;

namespace MusicStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SongController : Controller
    {
        private string conn_string = "Server=SHREYAS\\SQLEXPRESS;Database=MusicStore;Integrated Security=true;Encrypt=True;TrustServerCertificate=True;";
        private SqlConnection GetOpenConnection()
        {
            var conn = new SqlConnection(conn_string);
            conn.Open();
            return conn;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongViewModel>>> GetAllSongs()
        {
            List<SongViewModel> songs = new List<SongViewModel>();
            using (var conn = GetOpenConnection())
            {
                string query = "SELECT song_id, song_name, song_genre, song_album, song_artist, album_id, song_release_date, price, song_file_path FROM dbo.Songs";
                var cmd = new SqlCommand(query, conn);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        songs.Add(new SongViewModel
                        {
                            song_id = reader.GetInt64(0),
                            song_name = reader.GetString(1),
                            song_genre = reader.GetString(2),
                            song_album = reader.GetString(3),
                            song_artist = reader.GetString(4),
                            album_id = reader.GetInt64(5),
                            song_release_date = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6),
                            price = reader.IsDBNull(7) ? (float?)null : (float)reader.GetDouble(7),
                            // song_file should be handled differently, as you cannot store IFormFile in DB directly.
                            // Here we assume song_file_path is stored and you can retrieve the file based on this path.
                            song_file_path = reader.GetString(8) // You need to handle file retrieval separately
                        });
                    }
                }
            }

            return Ok(songs);
        }
    }


}
