import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-admin-new-dialog',
  templateUrl: './admin-new-dialog.component.html',
  styleUrls: ['./admin-new-dialog.component.css']
})
export class AdminNewDialogComponent implements OnInit {

  public form: FormGroup;

  constructor(private fb: FormBuilder,
              private http: HttpClient,
              private dialogRef: MatDialogRef<AdminNewDialogComponent>) { }

  ngOnInit() {
    this.form = this.fb.group({
      Name: ['', Validators.required],
      Surname: ['', Validators.required],
      Email: ['', [Validators.required, Validators.email]],
      City: ['', Validators.required],
      PhoneNumber: ['', Validators.required],
    });
  }

  onRegister() {
    if (this.form.valid) {
      this.http.post(environment.hostUrl + '/api/sysAdmin', this.form.value).subscribe(() => {
        alert('SysAdmin created!');
        this.dialogRef.close();
      }, () => {
        alert('Error while creating account!');
      });
    }
  }

}
