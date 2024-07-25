using APIs.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;

namespace APIs.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class Album: ControllerBase {
        private string conn_string = "Server=U1-8C9BGY3-L\\SQLEXPRESS;Database=MusicStore;Integrated Security=true;";
        private SqlConnection GetOpenConnection() {
            var conn = new SqlConnection(conn_string);
            conn.Open();
            return conn;
        }

        [HttpGet("{id}")]
        public AlbumModel getAlbum(long id) {
            AlbumModel model = null;
            using (var conn = GetOpenConnection()) {
                string query = "SELECT * from Albums WHERE album_id=@id;";
                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                using (var reader = cmd.ExecuteReader()) {
                    if (reader.Read()) {
                        model = new AlbumModel {
                            album_id = Convert.ToInt64(reader[0]),
                            album_name = Convert.ToString(reader[1]),
                            album_artist = Convert.ToString(reader[2]),
                            album_price = Convert.ToInt64(reader[3]),
                            album_release_date = Convert.ToDateTime(reader[4]).ToString("yyyy-MM-dd"),
                            album_genre = Convert.ToString(reader[5]),
                            cover_art = Convert.ToString(reader[6])
                        };
                    }
                }
            }
            return model;
        }

        [HttpGet]
        public IEnumerable<AlbumModel> getAlbums() {
            List<AlbumModel> albums = new List<AlbumModel>();
            using (var conn = GetOpenConnection()) {
                string query = "SELECT * from Albums;";
                var cmd = new SqlCommand(query, conn);
                using (var reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        albums.Add(new AlbumModel {
                            album_id = Convert.ToInt64(reader[0]),
                            album_name = Convert.ToString(reader[1]),
                            album_artist = Convert.ToString(reader[2]),
                            album_price = Convert.ToInt64(reader[3]),
                            album_release_date = Convert.ToDateTime(reader[4]).ToString("yyyy-MM-dd"),
                            album_genre = Convert.ToString(reader[5]),
                            cover_art = Convert.ToString(reader[6])
                        });
                    }
                }
            }
            return albums;
        }

        [HttpPost]
        public async Task<ActionResult> postAlbum([FromBody] AlbumPostModel model) {
            using (var conn = GetOpenConnection()) {
                string query = "INSERT INTO Albums (album_name, album_price, album_release_date, album_artist, album_genre, cover_art)" +
                                "VALUES (@album_name, @album_price, @album_release_date, @album_artist, @album_genre, @cover_art);";

                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@album_name", model.album_name);
                cmd.Parameters.AddWithValue("@album_price", model.album_price);
                cmd.Parameters.AddWithValue("@album_artist", model.album_artist);
                cmd.Parameters.AddWithValue("@album_genre", model.album_genre);
                cmd.Parameters.AddWithValue("@cover_art", model.cover_art);
                if (!string.IsNullOrEmpty(model.album_release_date)) {
                    DateTime release;
                    if (DateTime.TryParseExact(model.album_release_date, "yyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out release)) {
                        cmd.Parameters.Add(new SqlParameter("@album_release_date", System.Data.SqlDbType.Date) {
                            Value = release
                        });
                    }
                } else {
                    cmd.Parameters.Add(new SqlParameter("@album_release_date", System.Data.SqlDbType.Date) { Value = DBNull.Value});
                }

                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                if (rowsAffected > 0) {
                    return Ok(new { message = "Album added successfully" });
                } else {
                    return StatusCode(500, "Error while adding Album");
                }
            }
        }
    }
}
