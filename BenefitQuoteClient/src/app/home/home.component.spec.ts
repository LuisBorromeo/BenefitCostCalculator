import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeComponent } from './home.component';
import {CoreModule} from '../core/core.module';
import {SharedModule} from '../shared/shared.module';
import {RouterTestingModule} from '@angular/router/testing';
import {EmployeeService} from '../service/employee.service';
import {TestEmployeeService} from '../service/testing/test-employee.service';

describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;

  beforeEach(async(() => {
    const testEmployeeService = new TestEmployeeService(null);

    TestBed.configureTestingModule({
      imports: [CoreModule, SharedModule],
      declarations: [ HomeComponent ],
      providers: [
        {provide: EmployeeService, useValue: testEmployeeService}
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should get employees', () => {
    component.ngOnInit();

    expect(component.employees).toBeDefined();
  });
});
