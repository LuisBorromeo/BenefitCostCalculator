import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuoteComponent } from './quote.component';
import {EmployeeService} from '../service/employee.service';
import {SharedModule} from '../shared/shared.module';
import {CoreModule} from '../core/core.module';
import {RouterTestingModule} from '@angular/router/testing';

describe('QuoteComponent', () => {
  let component: QuoteComponent;
  let fixture: ComponentFixture<QuoteComponent>;
  let mockEmployeeService: jasmine.SpyObj<EmployeeService>;

  beforeEach(() => {
    const employeeServiceSpy = jasmine.createSpyObj('EmployeeService', ['get']);

    TestBed.configureTestingModule({
      imports: [CoreModule, SharedModule, RouterTestingModule],
      declarations: [ QuoteComponent],
      providers: [
        {provide: EmployeeService, use: employeeServiceSpy}
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

  it('should display name of employee', () => {
    expect(component.employeeId).toBeDefined();
  });
});
