namespace MusicStoreWebApp.Models {
    public class SongViewModel {
        public Int64 song_id { get; set; }
        public string song_name { get; set; }
        public string song_genre { get; set; }
        public string song_album { get; set; }
        public string song_artist { get; set; }
        public Int64 album_id { get; set; }
        public DateTime? song_release_date { get; set; }
        public float? price { get; set; }
        public IFormFile song_file { get; set; }
    }
}
