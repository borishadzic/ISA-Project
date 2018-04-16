import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { EditStageDialogComponent } from '../edit-stage-dialog/edit-stage-dialog.component';
import { AddStageDialogComponent } from '../add-stage-dialog/add-stage-dialog.component';

@Component({
  selector: 'app-stages',
  templateUrl: './stages.component.html',
  styleUrls: ['./stages.component.css']
})
export class StagesComponent implements OnInit {

  stages = [];

  constructor(private dialog: MatDialog) { }

  ngOnInit() {
  }

  openEditStageDialog(stage, idx) {
    let dialogRef = this.dialog.open(EditStageDialogComponent, {
      data: { stage: stage }
    });

    dialogRef.afterClosed().subscribe(x => {
      if (x) {
        this.stages[idx] = x;
      }
    });
  }

  openAddStageDialog() {
    let dialogRef = this.dialog.open(AddStageDialogComponent);

    dialogRef.afterClosed().subscribe(x => {
      if (x) {
        this.stages.push(x);
      }
    });
  }

}
