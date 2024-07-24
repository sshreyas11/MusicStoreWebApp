export class Album{
    album_name: string;
    album_price: number;
    album_artist: string;
    album_release_date: string;
    album_genre: string;
    cover_art:string;
    constructor(){
        this.album_name = '';
        this.album_artist = '';
        this.album_price = 0;
        this.album_release_date = '';
        this.album_genre = '';
        this.cover_art = '';
    }
}