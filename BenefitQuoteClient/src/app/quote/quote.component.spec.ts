import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuoteComponent } from './quote.component';

describe('QuoteComponent', () => {
  let component: QuoteComponent;
  let fixture: ComponentFixture<QuoteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuoteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuoteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('employee name should be displayed', () => {
    // expect(component).toBeTruthy();
  });

  it('if employee not found then display NotFound message', () => {
    // expect(component).toBeTruthy();
  });

  it('should enable add button when dependent name is valid.', () => {
    // expect(component).toBeTruthy();
  });

  it('should display total cost.', () => {
    // expect(component).toBeTruthy();
  });

  it('should display discount amount.', () => {
    // expect(component).toBeTruthy();
  });
});
