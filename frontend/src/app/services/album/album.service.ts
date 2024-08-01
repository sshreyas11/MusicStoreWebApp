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
  private getalbums_endpoint = 'http://localhost:5283/api/Album/all';
  private addalbums_endpoint = 'http://localhost:5283/api/Album/addAlbum';
  headers = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  constructor(private http: HttpClient) {}
  getAlbums(): Observable<Album[]> {
    return this.http.get<Album[]>(this.getalbums_endpoint);
  }

  postAlbum(album: Album): any {
    console.log("Album service ", album)
    return this.http.post(this.addalbums_endpoint, album, this.headers);
  }
}
  