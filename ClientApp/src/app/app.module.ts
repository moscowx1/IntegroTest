import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { ModelModule } from "./models/model.module";
import { AppComponent } from './app.component';
import { InvalidRowListComponent } from "./lists/invalidRow.list.component";
import { ExcelListComponent } from "./lists/excel.list.component";
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from "./app-routing.module";

@NgModule({
  declarations: [
    AppComponent,
    InvalidRowListComponent,
    ExcelListComponent,
  ],
  imports: [
    BrowserModule,
    ModelModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
