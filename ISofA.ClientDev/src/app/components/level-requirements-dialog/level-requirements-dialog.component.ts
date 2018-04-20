import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { LevelRequirementsService } from '../../services/level-requirements.service';
import { LevelRequirements } from '../../models/level-requirements';

@Component({
  selector: 'app-level-requirements-dialog',
  templateUrl: './level-requirements-dialog.component.html',
  styleUrls: ['./level-requirements-dialog.component.css']
})
export class LevelRequirementsDialogComponent implements OnInit {

  public form: FormGroup;
  public requirements: LevelRequirements;

  constructor(private fb: FormBuilder, private service: LevelRequirementsService) { }

  ngOnInit() {
    this.service.getLevelRequirements().subscribe(req => this.requirements = req);

    this.form = this.fb.group({
      BronzeLevel: [0, Validators.required],
      SilverLevel: [0, Validators.required],
      GoldLevel: [0, Validators.required]
    }, {
      validator: this.validate
    });
  }

  validate(group: FormGroup) {
    const bronze = group.controls.BronzeLevel.value;
    const silver = group.controls.SilverLevel.value;
    const gold = group.controls.GoldLevel.value;

    return bronze < silver && silver < gold ? null : { valid: false };
  }

  onSubmit() {
    if (this.form.valid) {
      this.service.setLevelRequirements(this.form.value)
        .subscribe(req => this.requirements = req);
    }
  }

}
