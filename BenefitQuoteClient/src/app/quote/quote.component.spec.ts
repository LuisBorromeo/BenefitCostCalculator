  import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuoteComponent } from './quote.component';
import {EmployeeService} from '../service/employee.service';
import {SharedModule} from '../shared/shared.module';
import {CoreModule} from '../core/core.module';
import {RouterTestingModule} from '@angular/router/testing';
import {TestEmployeeService} from '../service/testing/test-employee.service';
  import {By} from '@angular/platform-browser';

describe('QuoteComponent', () => {
  let component: QuoteComponent;
  let fixture: ComponentFixture<QuoteComponent>;
  let mockEmployeeService: EmployeeService;

  beforeEach(() => {
    const testEmployeeService = new TestEmployeeService(null);

    TestBed.configureTestingModule({
      imports: [CoreModule, SharedModule, RouterTestingModule],
      declarations: [ QuoteComponent],
      providers: [
        {provide: EmployeeService, useValue: testEmployeeService}
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(QuoteComponent);
    component = fixture.componentInstance;
    mockEmployeeService = TestBed.get(EmployeeService);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should get employee by valid employeeId', () => {
    component.employeeId = '1';
    component.getEmployeeData();

    expect(component.employee).toBeDefined();
    expect(component.isError).toBeFalsy();
  });

  it('should diplay 404 message if employee not found.', () => {
    component.employeeId = '2341234';
    component.getEmployeeData();

    expect(component.employee).toBeUndefined();
    expect(component.isError).toBeTruthy();
    expect(component.errorMessage).toBeDefined();
    expect(component.errorMessage).not.toBeNull();
    expect(component.errorMessage).not.toEqual('');
    expect(component.errorMessage.length).toBeGreaterThan(0);
  });

});

