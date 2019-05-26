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
    // return this.http.get(this.rootApiUrl + '/employee/' + employeeId );
    const url = this.rootApiUrl + '/employee/' + employeeId;

    return this.http.get<IEmployee>(url)
      .pipe(
        map(employee => employee),
        catchError(this.handleError<IEmployee>(`getEmployee id=${employeeId}`))
      );
  }

  all() {
    return this.http.get(this.rootApiUrl + '/employee/');
  }

  /**
   * Returns a function that handles Http operation failures.
   * This error handler lets the app continue to run as if no error occurred.
   * @param operation - name of the operation that failed
   */
  private handleError<T>(operation = 'operation') {
    return (error: HttpErrorResponse): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      const message = (error.error instanceof ErrorEvent) ?
        error.error.message :
        `server returned code ${error.status} with body "${error.error}"`;

      // TODO: better job of transforming error for user consumption
      throw new Error(`${operation} failed: ${message}`);
    };
  }
}
