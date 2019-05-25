import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';

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

  get(employeeId: number) {
    return this.http.get(this.rootApiUrl + '/employee/' + employeeId );
  }

  all() {
    return this.http.get(this.rootApiUrl + '/employee/');
  }
}
