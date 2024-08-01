import { Component, OnInit } from '@angular/core';
import { AlbumService } from '../services/album/album.service';
import { Album } from '../models/album.model';
import { NavbarService } from '../services/navbar/navbar.service';

@Component({
  selector: 'app-employee-dashboard',
  templateUrl: './employee-dashboard.component.html',
  styleUrls: ['./employee-dashboard.component.scss'],
})
export class EmployeeDashboardComponent {
  albumCollection: Album[] = [];
  newAlbum: Album = {} as Album;
  selectedFile: File | null = null;

  constructor(private albumService: AlbumService, private navbarService: NavbarService) {}

  ngOnInit(): void {
    this.loadAlbums();
  }

  loadAlbums():void{
    this.albumService.getAlbums().subscribe((album_data: Album[])=>{
      this.albumCollection = album_data;
      console.log(this.albumCollection)
    }, error => {
      console.log("Error");
    });  
  }

  addAlbum(): void {
    if (!this.selectedFile) {
      alert('Please select an image file.');
      return;
    }

    this.newAlbum.cover_art = this.newAlbum.album_name.replace(/\s+/g, '_') + '.jpeg';

    this.albumService.postAlbum(this.newAlbum).subscribe(() => {
      this.loadAlbums(); 
      this.newAlbum = {} as Album; 
    });
  }

    
}
