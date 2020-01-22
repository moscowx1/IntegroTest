import { Excel } from "./excel.model";
import {  Cell, InvalidRow, Table } from './table.model';

import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpRequest, HttpEvent } from "@angular/common/http";
import { Observable } from 'rxjs';


const excelApiUrl = "api/excel_Files";
const excelRowsApiUrl = "api/excel_rows";
const excelDownloadApiUrl = "api/download";

@Injectable()
export class Repository {
  excels: Excel[];
  table: Table = null;
  constructor(private http: HttpClient) {
    this.getExcelFiles();
  }


  getExcelFiles() {
    this.http.get<Excel[]>(`${excelApiUrl}`)
      .subscribe(e => {
        this.excels = e
      });
  }

  getTable(id: number) {
    this.http.get<Table>(`${excelRowsApiUrl}?id=${id}`)
      .subscribe(t => {
        this.table = new Table(
          t.excelId,
          t.header,
          t.invalidRows
        );
      });
  }

  downloadFile(table: Table): Observable<HttpEvent<Blob>> {
    let data = {
      excelId: table.excelId,
      header: null,
      invalidRows: table.invalidRows.filter(r => r.isSelected == true)
    };
    console.log(data);
    return this.http.request(new HttpRequest(
      'POST',
      excelDownloadApiUrl,
      data,
      {
        reportProgress: true,
        responseType: 'blob'
      }
    ))
  }
}
