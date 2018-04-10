import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { ItemService } from '../item.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-item-new',
  templateUrl: './item-new.component.html',
  styleUrls: ['./item-new.component.css']
})
export class ItemNewComponent implements OnInit {

  public form: FormGroup;
  private image: File;
  private theaterId: number;

  constructor(private fb: FormBuilder, private itemService: ItemService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.form = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      price: [0, Validators.min(0)]
    });

    this.theaterId = this.route.snapshot.parent.params['id'];
  }

  public onFileChanged(event) {
    this.image = event.target.files[0];
  }

  public isValid(): boolean {
    return this.image && this.form.valid;
  }

  public onSubmit() {
    if (this.isValid()) {
      this.itemService.addItem(this.theaterId, this.form.value).subscribe(item => {
        this.itemService.uploadImage(this.theaterId, item.ItemId, this.image).subscribe(newItem => {
          alert('Item created!');
          this.form.reset();
          console.log(newItem);
        });
      });
    }
  }

}
