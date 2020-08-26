import { Component, OnInit } from '@angular/core';
import { Child } from "../child";
import { AppService } from "../app.service";
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-new-component',
  templateUrl: './new-component.component.html',
  styleUrls: ['./new-component.component.css']
})

export class NewComponentComponent implements OnInit {
  title = 'project';
  children: Child[] = [];
  formLabel: string;
  isEditMode = false;
  form: FormGroup;
  child: Child = null;

  constructor(private appService: AppService, private formBuilder: FormBuilder) {
    this.form = formBuilder.group({
      "Lastname": ["", Validators.required],
      "Firstname": ["", Validators.required],
      "Gender": ["", Validators.required],
      "Birthdate": ["", Validators.required]
    });

    this.formLabel = "Add Record";
  }

  ngOnInit() {
    this.getChildren();
  }

  onSubmit() {
    this.child = this.form.value;
    this.appService.addChild(this.child).subscribe(() => {
      window.location.reload();
    }, error => alert(error));
  }

  cancel() {
    this.formLabel = "Add Record";
    this.isEditMode = false;
    this.child = null;
    this.form.get("Lastname").setValue('');
    this.form.get("Firstname").setValue('');
    this.form.get('Gender').setValue('');
    this.form.get('Birthdate').setValue('');
  }

  delete(child: Child) {
    if (confirm("Are you sure want to delete this?")) {
      this.appService.deleteChild(child.id).subscribe(() => {
        window.location.reload();
      }, error => alert(error));
    }
  }

  private getChildren() {
    this.appService.getChildren().subscribe((result) => {
      this.children = result;
    }, error => alert(error));
  }
}
