import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {Observable} from 'rxjs';
import {IEmployee} from '../core/model/IEmployee';
import {catchError, map, tap} from 'rxjs/operators';
import {Employee} from '../core/model/Employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private rootApiUrl: string = '';

  constructor(private http: HttpClient) {
    if (environment.rootApiAddress) {
      this.rootApiUrl = environment.rootApiAddress;
    }
  }

  get(employeeId: number): Observable<Employee> {
    const url = this.rootApiUrl + '/employee/' + employeeId;
    return this.http.get<Employee>(url);
  }

  all() {
    return this.http.get<Employee[]>(this.rootApiUrl + '/employee/');
  }

}
