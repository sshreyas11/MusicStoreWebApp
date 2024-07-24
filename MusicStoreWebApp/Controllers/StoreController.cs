/*using Microsoft.AspNetCore.Mvc;
using MusicStoreWebApp.Models;
using System.Data.SqlClient;

namespace MusicStoreWebApp.Controllers {
    public class StoreController: Controller {

        private string conn_string = "Server=SHREYAS\\SQLEXPRESS;Database=MusicStore;Integrated Security=true;";
        private SqlConnection GetOpenConnection() {
            var conn = new SqlConnection(conn_string);
            conn.Open();
            return conn;
        }

        public IActionResult StoreIndex() {

            var albums = GetAlbums(); 
            var songs = GetSongs();

            var model = new StoreViewModel {
                albums = albums,
                songs = songs
            };
            return View(model);
        }

        private IEnumerable<AlbumViewModel> GetAlbums() {
            IList<AlbumViewModel> album_list = new List<AlbumViewModel>();
            using (var conn = GetOpenConnection()) {
                var cmd = new SqlCommand("select * from Albums", conn);
                var cReader = cmd.ExecuteReader();
                while (cReader.Read()) {
                    AlbumViewModel albumModel = new AlbumViewModel {
                        album_id = Convert.ToInt64(cReader[0]),
                        album_name = Convert.ToString(cReader[1]),
                        album_artist = Convert.ToString(cReader[2]),
                        album_price = Convert.ToDouble(cReader[3]),
                        album_release_date = Convert.ToString(cReader[4]),
                        album_genre = Convert.ToString(cReader[5])
                    };
                    album_list.Add(albumModel);
                }
            }
            return album_list;
        }

        private IEnumerable<SongViewModel> GetSongs() {
            IList<SongViewModel> song_list = new List<SongViewModel>();
            using (var conn = GetOpenConnection()) {
                var cmd = new SqlCommand("select * from Songs", conn);
                var cReader = cmd.ExecuteReader();
                while (cReader.Read()) {
                    SongViewModel songModel = new SongViewModel {
                        song_id = Convert.ToInt64(cReader[0]),
                        song_name = Convert.ToString(cReader[1]),
                        song_genre = Convert.ToString(cReader[2]),
                        song_album = Convert.ToString(cReader[3]),
                        song_artist = Convert.ToString(cReader[4]),
                        album_id = Convert.ToInt64(cReader[5]),
                        song_release_date = Convert.ToString(cReader[6]),
                        price = Convert.ToDouble(cReader[7])
                    };
                    song_list.Add(songModel);
                }
            }
            return song_list;
        }
     }
}
*/