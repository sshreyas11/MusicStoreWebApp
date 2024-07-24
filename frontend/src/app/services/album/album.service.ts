import { Injectable } from '@angular/core';
import { Album } from 'src/app/models/album.model';
import {
  HttpClient,
  HttpClientModule,
  HttpHeaders,
} from '@angular/common/http';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AlbumService {
  private getalbums_endpoint = 'http://localhost:8888/api/getAlbums';
  private addalbums_endpoint = 'http://localhost:8888/api/postAlbum';
  headers = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  constructor(private http: HttpClient) {}
  getAlbums(): Observable<Album[]> {
    return this.http.get<Album[]>(this.getalbums_endpoint);
  }

  postAlbum(text: string): any {
    return this.http.post(this.addalbums_endpoint, text, this.headers);
  }
}
  // private album_list:Album[] =[
  //   {id: 1, title: '1989', artist: 'Taylor Swift', releaseDate: '2014-10-14', genre: 'Pop', price:40},
  //   {id: 2, title: 'Thriller', artist: 'Michael Jackson', releaseDate: '1982-11-29', genre: 'Pop', price:40},
  //   {id: 3, title: 'Dark Side of the Moon', artist: 'Pink Floyd', releaseDate: '1973-03-01', genre: 'Progressive Rock',price:40},
  //   {id: 4, title: 'Folklore', artist: 'Taylor Swift', releaseDate: '2020-07-24', genre: 'Indie Folk', price:40},
  //   {id: 5, title: 'The Wall', artist: 'Pink Floyd', releaseDate: '1979-11-30', genre: 'Progressive Rock', price:40},
  // ];