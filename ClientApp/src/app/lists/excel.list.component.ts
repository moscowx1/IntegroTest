import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { Repository } from "../models/repository"
import { Excel } from "../models/excel.model";
import { AppComponent } from "../app.component";


@Component({
  selector: 'excel-list',
  templateUrl: './excel.list.component.html'
})
export class ExcelListComponent {
  selected: number = -1;
  constructor(private repo: Repository, private router: Router) { }

  get excelFiles(): Excel[] {
    return this.repo.excels;
  }

  select(id: number) {
    this.selected = id;
  }

  redirect() {
    this.repo.getTable(this.selected);
    this.router.navigateByUrl(`/row_list?id=${this.selected}`);
  }
}
