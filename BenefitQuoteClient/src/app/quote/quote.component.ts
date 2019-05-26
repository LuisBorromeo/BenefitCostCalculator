/* tslint:disable:no-trailing-whitespace */
import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {switchMap} from 'rxjs/operators';
import {ActivatedRoute, ParamMap, Router} from '@angular/router';
import {IEmployee} from '../core/model/IEmployee';
import {EmployeeService} from '../service/employee.service';
import {Employee} from '../core/model/Employee';

@Component({
  selector: 'app-quote',
  templateUrl: './quote.component.html',
  styleUrls: ['./quote.component.css']
})
export class QuoteComponent implements OnInit {
  dependentNameInput: string;

  displayedColumns: string[] = ['name', 'columndelete'];
  dependentData: string[] = [];
  employeeId: number;
  employee: Employee;

  dataTableSource: string[];

  private changeDetectorRefs: ChangeDetectorRef;

  private readonly employeeService: EmployeeService;
  constructor(changeDetectorRefs: ChangeDetectorRef,
              private readonly route: ActivatedRoute,
              private readonly router: Router,
              employeeService: EmployeeService) {
    this.employeeService = employeeService;
    this.changeDetectorRefs = changeDetectorRefs;
  }

  ngOnInit() {
    // this.refresh();
    let employeeIdRouteParam$ = this.route.paramMap.pipe(
      switchMap(params => {
        return params.get('employeeId');
        /*const id = +params.get('employeeId')
        return this.service.getData(id) // http request*/
      })
    );

    employeeIdRouteParam$.subscribe(employeeIdRouteParam => {
      this.employeeId = +employeeIdRouteParam;
      this.init();
    });

    // For subscribing to the observable paramMap...
    // this._route.paramMap.pipe(
    //   switchMap((params: ParamMap) => {
    //     this.employeeId = +params.get('employeeId');
    //   })
    // );

  }

  init() {
    if (this.employeeId) {
      this.employeeService.get(this.employeeId)
        .subscribe(result => this.employee = result);
    }
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
