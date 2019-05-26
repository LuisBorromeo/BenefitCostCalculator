import { Injectable } from '@angular/core';

import {Observable, of} from 'rxjs';

import { map } from 'rxjs/operators';
import {EmployeeService} from '../employee.service';
import {asyncData} from '../../../testing/async-observable-helpers';
import {Employee} from '../../core/model/Employee';
import {HttpClient} from '@angular/common/http';

@Injectable()
export class TestEmployeeService extends EmployeeService {

  constructor(http: HttpClient) {
    super(http);
  }

  employees: Employee[] = [
    new Employee(1, 'alex smith'),
    new Employee(2, 'John Doe'),
    ];
  lastResult: Observable<Employee>; // result from last method call

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

  get(employeeId: number): Observable<Employee> {
    // let employee1 = this.employees.find(e => e.id === employeeId);
    // return this.lastResult = asyncData(this.employees[0]);
    return of(this.employees.find(e => e.id === employeeId));
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
