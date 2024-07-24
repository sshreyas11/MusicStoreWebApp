import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AlbumService } from 'src/app/services/album/album.service';

@Component({
  selector: 'app-add-album',
  templateUrl: './add-album.component.html',
  styleUrls: ['./add-album.component.scss']
})
export class AddAlbumComponent {

  //addAlbumForm: FormGroup;
  error: string|null = null;
  success_message: string = '';
  error_message: string = '';

  constructor(public fb: FormBuilder, private album_service:AlbumService){
    // this.addAlbumForm = this.fb.group({
    //   album_id: ['', Validators.required],
    //   album_name: ['', Validators.required],
    //   album_price: ['', [Validators.required, Validators.pattern(/^\d+(\.\d{1,2})?$/)]],
    //   album_release_date: ['', Validators.required],
    //   album_artist: ['', Validators.required],
    //   album_genre: ['', Validators.required],
    //   cover_art: ['', Validators.required]
    // });
  }

  


  onSubmit():void{
    const text = "sike";
    this.album_service.postAlbum(text).subscribe((response: any)=>{
      console.log("Sent successfull, backend message ", response);
    }, (error: any)=>{
      console.log("Error, backend message - ", error);
    });
  }
}
