import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UserItemService } from '../user-item.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-user-item-new',
  templateUrl: './user-item-new.component.html',
  styleUrls: ['./user-item-new.component.css']
})
export class UserItemNewComponent implements OnInit {

  public form: FormGroup;
  private image: File;
  private theaterId: number;

  constructor(private fb: FormBuilder,
              private userItemService: UserItemService,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.form = this.fb.group({
      Name: ['', Validators.required],
      Description: ['', Validators.required],
      ExpirationDate: [null, [Validators.required]]
    });

    this.theaterId = this.route.snapshot.parent.params['id'];
  }

  public onFileChanged(event) {
    this.image = event.target.files[0];
  }

  onSubmit() {
    if (this.form.valid) {
      this.userItemService.addUserItem(this.theaterId, this.form.value).subscribe(res => {
        if (this.image) {
          this.userItemService.uploadImage(res.UserItemId, this.image).subscribe(userItem => {
            alert('User item submitd for review!');
            this.form.reset();
          });
        } else {
          alert('User item submitd for review!');
          this.form.reset();
        }
      });
    }
  }

}
