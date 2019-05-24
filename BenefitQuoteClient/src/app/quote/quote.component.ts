import {ChangeDetectorRef, Component, OnInit} from '@angular/core';

@Component({
  selector: 'app-quote',
  templateUrl: './quote.component.html',
  styleUrls: ['./quote.component.css']
})
export class QuoteComponent implements OnInit {
  dependentNameInput: string;

  displayedColumns: string[] = ['name', 'columndelete'];
  dependentData: string[] = ['sdfasd'];

  dataTableSource: string[];

  constructor(private changeDetectorRefs: ChangeDetectorRef) { }

  ngOnInit() {
    this.refresh();
  }

  addDependent() {
    // validate: check if null ot empty
    this.dependentData.push(this.dependentNameInput);
    this.dependentNameInput = '';
    this.refresh();
  }

  refresh() {
    this.dataTableSource = [...this.dependentData];
    this.changeDetectorRefs.detectChanges();
  }

  delete(element: any) {
    // console.log(element);
    let i = this.dependentData.indexOf(element);
    this.dependentData.splice(i, 1);
    this.refresh();
    /*this.dataSource.data = this.dataSource.data
      .filter(i => i !== elm)
      .map((i, idx) => (i.position = (idx + 1), i));*/
  }
}
