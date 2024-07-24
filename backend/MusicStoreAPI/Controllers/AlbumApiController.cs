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
    }
}
