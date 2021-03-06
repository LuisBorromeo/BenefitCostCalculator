/* tslint:disable:no-trailing-whitespace */
import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {catchError, map, switchMap} from 'rxjs/operators';
import {ActivatedRoute, ParamMap, Router} from '@angular/router';
import {IEmployee} from '../core/model/IEmployee';
import {EmployeeService} from '../service/employee.service';
import {Employee} from '../core/model/Employee';
import {HttpErrorResponse} from '@angular/common/http';
import {Observable} from 'rxjs';
import {QuoteService} from '../service/quote.service';
import {BenefitsCostQuote} from '../core/model/BenefitsCostQuote';
import {RuleValueResult} from '../core/model/RuleValueResult';

@Component({
  selector: 'app-quote',
  templateUrl: './quote.component.html',
  styleUrls: ['./quote.component.css']
})
export class QuoteComponent implements OnInit {
  isError = false;
  errorMessage: string;

  isValidDependentName: boolean;
  dependentNameInput: string;

  displayedColumns: string[] = ['name', 'cost', 'discount', 'isDiscountApplied', 'columndelete'];
  dependentData: string[] = [];
  employeeId: string;
  employee: Employee;

  dataTableSource: RuleValueResult[];

  private changeDetectorRefs: ChangeDetectorRef;
  private readonly employeeService: EmployeeService;
  private quoteService: QuoteService;
  private benefitCostQuote: BenefitsCostQuote;

  constructor(changeDetectorRefs: ChangeDetectorRef,
              private readonly route: ActivatedRoute,
              private readonly router: Router,
              employeeService: EmployeeService,
              quoteService: QuoteService) {
    this.employeeService = employeeService;
    this.changeDetectorRefs = changeDetectorRefs;
    this.quoteService = quoteService;
  }

  ngOnInit() {
    // this.refresh();
    let employeeIdRouteParam$ = this.route.paramMap.pipe(
      map(params => {
        return params.get('employeeId');
        /*const id = +params.get('employeeId')
        return this.service.getEmployeeData(id) // http request*/
      })
    );

    employeeIdRouteParam$.subscribe(employeeIdRouteParam => {
      this.employeeId = employeeIdRouteParam;
      this.getEmployeeData();
    });
  }

  getEmployeeData() {
    if (this.employeeId) {
      this.employeeService
        .get(this.employeeId)
        .pipe(
          map(employee => employee),
          catchError(this.handleError<Employee>(`Employee`))
        )
        .subscribe(result => {
          this.employee = result;
          this.generateQuote();
        });
    }
  }

  addDependent() {
    // validate: check if null ot empty

    this.dependentData.push(this.dependentNameInput);
    this.dependentNameInput = '';
    this.generateQuote();
  }

  private generateQuote() {
    this.quoteService
      .generateQuote(this.employeeId, this.dependentData)
      .pipe(
        map(benefitCostQuote => benefitCostQuote),
        catchError(this.handleError<BenefitsCostQuote>(`Quote`))
      )
      .subscribe(result => {
        this.benefitCostQuote = result;
        this.refresh();
      });
  }

  refresh() {
    this.dataTableSource = [...this.benefitCostQuote.quoteItems];
    this.changeDetectorRefs.detectChanges();
  }

  removeDependent(element: any) {
    // console.log(element);
    let i = this.dependentData.indexOf(element);
    this.dependentData.splice(i, 1);
    this.generateQuote();
  }

  validateDependentName($event: Event) {
    let isValid = true;
    const target = $event.target as HTMLTextAreaElement;
    const inputValue = target.value;

    if (!inputValue){
      isValid = false;
    }

    if ('' === inputValue.trim()){
      isValid = false;
    }

    this.isValidDependentName = isValid;
  }

  private handleError<T>(resourceName = 'resource') {
    return (error: HttpErrorResponse): Observable<T> => {
      this.isError = true;

      // TODO: send the error to remote logging infrastructure
      // console.error(error); // log to console instead

      // const message = (error.error instanceof ErrorEvent) ?
      //   error.error.message : `server returned code ${error.status} with body "${error.error}"`;

      switch (error.status) {
        case 404: {
          this.errorMessage = 'The ' + resourceName + ' could could not be found.';
          break;
        }
        case 401: {
          this.errorMessage = 'We do not have access to ' + resourceName + '.';
          break;
        }
        default: {
          this.errorMessage = 'Oops! Something went wrong accessing ' + resourceName + '.';
          console.error(error);
          break;
        }
      }

      // TODO: better job of transforming error for user consumption
      throw new Error(`${resourceName} failed: ${this.errorMessage}`);
    };
  }
}
