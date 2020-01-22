import { Component } from "@angular/core";

import { Repository } from "../models/repository";
import { Cell, InvalidRow, Table } from "../models/table.model";
import { Router } from '@angular/router';
import { HttpEventType } from '@angular/common/http';

@Component({
  selector: 'list-row',
  templateUrl: './invalidRow.list.component.html'
})
export class InvalidRowListComponent {
  constructor(
    private repo: Repository,
    private route: Router) {
  }

  get table(): Table {
    return this.repo.table;
  }
  download() {
    //this.repo.postTable(this.table);
    this.repo.downloadFile(this.table).subscribe(
      data => {
        switch (data.type) {
          case HttpEventType.Response:
            const downloadedFile = new Blob([data.body], {
              type: data.body.type
            });
            const a = document.createElement('a');
            a.setAttribute('style', 'display:none;');
            document.body.appendChild(a);
            a.href = URL.createObjectURL(downloadedFile);
            a.target = '_blank';
            a.click();
            document.body.removeChild(a);
            break;
        }
      }
    );
  }
}
