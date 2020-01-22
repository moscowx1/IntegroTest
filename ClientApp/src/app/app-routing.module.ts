import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ExcelListComponent } from "./lists/excel.list.component";
import { InvalidRowListComponent } from "./lists/invalidRow.list.component";

const routes: Routes = [
  { path: "", component: ExcelListComponent },
  { path: "row_list", component: InvalidRowListComponent }
    ];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
