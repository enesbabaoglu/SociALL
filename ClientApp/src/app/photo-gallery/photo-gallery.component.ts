import { Component, Input, OnInit } from '@angular/core';
import { image } from '../_models/image';

@Component({
  selector: 'photo-gallery',
  templateUrl: './photo-gallery.component.html',
  styleUrls: ['./photo-gallery.component.css']
})
export class PhotoGalleryComponent implements OnInit {

  @Input() images : image[] | undefined;
  constructor() { }

  ngOnInit(): void {
  }

}
