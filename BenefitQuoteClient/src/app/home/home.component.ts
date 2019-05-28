import { Component, OnInit } from '@angular/core';
import {EmployeeService} from '../service/employee.service';
import {Employee} from '../core/model/Employee';
import {RuleValueResult} from '../core/model/RuleValueResult';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  private _employeeService: EmployeeService;
  private employees: Employee[];

  displayedColumns: string[] = ['employeeId', 'name'];
  dataTableSource: Employee[];

  constructor(employeeService: EmployeeService) {
    this._employeeService = employeeService;
  }

  ngOnInit() {
    this._employeeService
      .all()
      .subscribe( result => {
        this.employees = result;
        this.dataTableSource = [...this.employees];
      });
  }

}
