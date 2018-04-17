import { Component, OnInit } from '@angular/core';
import { MatDialog, MatSnackBar } from '@angular/material';
import { StageService } from '../../../services/stage.service';
import { StageEditDialogComponent } from '../stage-edit-dialog/stage-edit-dialog.component';
import { StageAddDialogComponent } from '../stage-add-dialog/stage-add-dialog.component';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-stage-list',
  templateUrl: './stage-list.component.html',
  styleUrls: ['./stage-list.component.css']
})
export class StageListComponent implements OnInit {

  stages: any[];

  constructor(private dialog: MatDialog,
    private snackBar: MatSnackBar,
    private authService: AuthService,
    private stageService: StageService) { }

  ngOnInit() {
  }

  getStages() {
    if (this.stages)
      return;
    this.stageService.getStages(this.authService.adminOfTheater).subscribe((x) => {
      this.stages = x;
    });
  }

  openEditStageDialog(stage, idx) {
    let dialogRef = this.dialog.open(StageEditDialogComponent, {
      data: stage
    });

    dialogRef.afterClosed().subscribe(x => {
      if (x) {
        this.stageService.updateStage(this.authService.adminOfTheater, stage.StageId, x).subscribe((y) => {
          this.stages[idx].Name = y.Name;
          this.stages[idx].SeatRows = y.SeatRows;
          this.stages[idx].SeatColumns = y.SeatColumns;
          this.snackBar.open(`Updated Stage "${y.Name}"`, undefined, { duration: 1500 });
        });
      }
    });
  }

  openAddStageDialog() {
    let dialogRef = this.dialog.open(StageAddDialogComponent);

    dialogRef.afterClosed().subscribe(x => {
      if (x) {
        this.stageService.addStage(this.authService.adminOfTheater, x).subscribe((y) => {
          x.StageId = y.StageId;
          this.stages.push(x);
          this.snackBar.open(`Created Stage "${y.Name}"`, undefined, { duration: 1500 });
        })
      }
    });
  }

}
