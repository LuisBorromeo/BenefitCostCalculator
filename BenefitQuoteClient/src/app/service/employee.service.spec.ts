import { TestBed } from '@angular/core/testing';

import { EmployeeService } from './employee.service';
import {HttpErrorResponse} from '@angular/common/http';
import {asyncData, asyncError} from '../../testing/async-observable-helpers';
import {Employee} from '../core/model/Employee';

describe('EmployeeService', () => {
  let httpClientSpy: { get: jasmine.Spy };
  let employeeService: EmployeeService;

  beforeEach(() => {
    httpClientSpy = jasmine.createSpyObj('HttpClient', ['get']);
    employeeService = new EmployeeService(<any> httpClientSpy);
  });

  /* This only fails if ng test is run, but passes if run in the WebStorm test runner. */
  it('should return a collection of employees', () => {
    const employees: Employee[] = [{id: 1, name: 'Alvin Jones'}, {id: 2, name: 'Alex Smith'}];

    httpClientSpy
      .get
      .and
      .returnValues(asyncData(employees));

    employeeService.all().subscribe(
      result => expect(result.length).toEqual(employees.length, 'expected employee'),
      fail
    );
    expect(httpClientSpy.get.calls.count()).toBe(1, 'one call');
  });

  it('should return expected employee requested by employeeId', () => {
    const employee: Employee = {id: 1, name: 'Alvin Smith'}

    httpClientSpy
      .get
      .and
      .callFake((url: any) => {
        return asyncData(employee);
      });
    // httpClientSpy.get.and.returnValue(employee);

    employeeService.get(employee.id).subscribe(
      result => expect(result).toEqual(employee, 'expected employee'),
      fail
    );
    expect(httpClientSpy.get.calls.count()).toBe(1, 'one call');
  });

  it('should return an error when the server returns a 404', () => {
    const errorResponse = new HttpErrorResponse({
      error: 'test 404 error',
      status: 404,
      statusText: 'Not Found'
    });

    // httpClientSpy.get.and.returnValue(asyncError(errorResponse));
    httpClientSpy.get.and.callFake((url: string) => asyncError(errorResponse));

    employeeService.get(-1).subscribe(
      employee => fail('expected an error, not Employee'),
      error  => expect(error.message).toContain('404')
    );
  });
});
