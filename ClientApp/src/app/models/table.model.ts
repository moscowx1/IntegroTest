export class Cell {
  constructor(
    public columnNumber: number,
    public content: string) {
  }
}

export class InvalidRow {
  constructor(
    public rowNumber: number,
    public cells: Cell[],
    public isSelected: boolean = true) { }
}

export class Table {
  constructor(
    public excelId: number,
    public header: Cell[],
    public invalidRows: InvalidRow[]
  ) {}
}
