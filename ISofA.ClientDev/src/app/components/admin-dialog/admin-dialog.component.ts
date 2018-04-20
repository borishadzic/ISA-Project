import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MatDialog } from '@angular/material';

import { LevelRequirementsDialogComponent } from '../level-requirements-dialog/level-requirements-dialog.component';

@Component({
  selector: 'app-admin-dialog',
  templateUrl: './admin-dialog.component.html',
  styleUrls: ['./admin-dialog.component.css']
})
export class AdminDialogComponent implements OnInit {

  constructor(private dialogRef: MatDialogRef<AdminDialogComponent>,
              private dialog: MatDialog) { }

  ngOnInit() {
  }

  onOpenDialog() {
    this.dialogRef.close();
    this.dialog.open(LevelRequirementsDialogComponent);
  }

}
