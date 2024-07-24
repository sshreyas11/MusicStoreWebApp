using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MusicStoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStoreWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumApiController : ControllerBase
    {
        /*private string conn_string = "Server=SHREYAS\\SQLEXPRESS;Database=MusicStore;Integrated Security=true;";

        private SqlConnection GetOpenConnection()
        {
            var conn = new SqlConnection(conn_string);
            conn.Open();
            return conn;
        }*/
        private string conn_string = "Server=SHREYAS\\SQLEXPRESS;Database=MusicStore;Integrated Security=true;Encrypt=True;TrustServerCertificate=True;";

        private SqlConnection GetOpenConnection()
        {
            var conn = new SqlConnection(conn_string);
            conn.Open();
            return conn;
        }

        // GET: api/AlbumApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlbumViewModel>> GetAlbum(long id)
        {
            AlbumViewModel album = null;
            using (var conn = GetOpenConnection())
            {
                string query = "SELECT album_id, album_name, album_price, album_release_date, album_artist, album_genre FROM Albums WHERE album_id = @id";
                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        album = new AlbumViewModel
                        {
                            album_id = reader.GetInt64(0),
                            album_name = reader.GetString(1),
                            album_price = reader.GetDouble(2),
                            album_release_date = reader.GetDateTime(3).ToString("yyyy-MM-dd"),
                            album_artist = reader.GetString(4),
                            album_genre = reader.GetString(5)
                        };
                    }
                }
            }

            if (album == null)
            {
                return NotFound();
            }

            return Ok(album);
        }

        // Optional: GET: api/AlbumApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumViewModel>>> GetAllAlbums()
        {
            List<AlbumViewModel> albums = new List<AlbumViewModel>();
            using (var conn = GetOpenConnection())
            {
                string query = "SELECT album_id, album_name, album_price, album_release_date, album_artist, album_genre FROM Albums";
                var cmd = new SqlCommand(query, conn);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        albums.Add(new AlbumViewModel
                        {
                            album_id = reader.GetInt64(0),
                            album_name = reader.GetString(1),
                            album_price = reader.GetDouble(2),
                            album_release_date = reader.GetDateTime(3).ToString("yyyy-MM-dd"),
                            album_artist = reader.GetString(4),
                            album_genre = reader.GetString(5)
                        });
                    }
                }
            }

            return Ok(albums);
        }
        // POST: api/AlbumApi
        [HttpPost]
        public async Task<ActionResult> AddAlbum([FromBody] AlvumPostVM album)
        {
            using (var conn = GetOpenConnection())
            {
                string query = "INSERT INTO Albums (album_name, album_price, album_release_date, album_artist, album_genre) " +
                               "VALUES (@album_name, @album_price, @album_release_date, @album_artist, @album_genre)";

                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@album_name", album.album_name);
                cmd.Parameters.AddWithValue("@album_price", album.album_price);
                cmd.Parameters.AddWithValue("@album_artist", album.album_artist);
                cmd.Parameters.AddWithValue("@album_genre", album.album_genre);

                // Handle the date parameter explicitly
                if (!string.IsNullOrEmpty(album.album_release_date))
                {
                    DateTime releaseDate;
                    if (DateTime.TryParseExact(album.album_release_date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out releaseDate))
                    {
                        cmd.Parameters.Add(new SqlParameter("@album_release_date", System.Data.SqlDbType.Date) { Value = releaseDate });
                    }
                    else
                    {
                        return BadRequest("Invalid date format for album_release_date. Please use yyyy-MM-dd format.");
                    }
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@album_release_date", System.Data.SqlDbType.Date) { Value = DBNull.Value });
                }

                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                if (rowsAffected > 0)
                {
                    return Ok(new { message = "Album added successfully." });
                }
                else
                {
                    return StatusCode(500, "A problem happened while handling your request.");
                }
            }
        }
}
    }
