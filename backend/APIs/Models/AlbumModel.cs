namespace APIs.Models {
    public class AlbumModel {
        public Int64 album_id { get; set; }
        public string album_name { get; set; }

        public Int64 album_price { get; set; }

        public string album_release_date { get; set; }

        public string album_artist { get; set; }

        public string album_genre { get; set; }

        public string cover_art { get; set; }

    }
}
