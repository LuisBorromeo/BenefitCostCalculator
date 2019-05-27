import { Injectable } from '@angular/core';

import {Observable, of} from 'rxjs';

import { map } from 'rxjs/operators';
import {EmployeeService} from '../employee.service';
import {asyncData} from '../../../testing/async-observable-helpers';
import {Employee} from '../../core/model/Employee';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';

@Injectable()
export class TestEmployeeService extends EmployeeService {

  constructor(http: HttpClient) {
    super(http);
  }

  employees: Employee[] = [
    new Employee('1', 'alex smith'),
    new Employee('2', 'John Doe'),
    ];

  /*
  addHero(hero: Hero): Observable<Hero> {
    throw new Error('Method not implemented.');
  }

  deleteHero(hero: number | Hero): Observable<Hero> {
    throw new Error('Method not implemented.');
  }

  getHeroes(): Observable<Hero[]> {
    return this.lastResult = asyncData(this.heroes);
  }*/

  get(employeeId: string): Observable<Employee> {
    // const employee = this.employees.find(e => e.id === employeeId);
    // if (employee){
    //   this.lastResult = of(employee);
    // } else {
    //   throw new HttpErrorResponse({status: 404, statusText: 'Not Found', url: '/employee'});
    // }
    // this.lastResult = of(this.employees.find(e => e.id === employeeId));

    return of(this.employees.find(e => e.id === employeeId))
      .pipe(
        map(empl => {
          if (empl) {
            return empl;
          } else {
            throw new HttpErrorResponse({status: 404, statusText: 'Not Found', url: '/employee'});
          }
        })
      );
  }

  all(): Observable<Employee[]> {
    return of(this.employees);
  }

  /*updateHero(hero: Hero): Observable<Hero> {
    return this.lastResult = this.getHero(hero.id).pipe(
      map(h => {
        if (h) {
          return Object.assign(h, hero);
        }
        throw new Error(`Hero ${hero.id} not found`);
      })
    );
  }*/
}
