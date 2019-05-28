import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {Observable} from 'rxjs';
import {BenefitsCostQuote} from '../core/model/BenefitsCostQuote';
import {Employee} from '../core/model/Employee';
import {BenefitsCostQuoteRequest} from '../core/model/BenefitsCostQuoteRequest';

@Injectable({
  providedIn: 'root'
})
export class QuoteService {
  private rootApiUrl: string = '';

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json; charset=utf-8'
    })
  };

  constructor(private http: HttpClient) {
    if (environment.rootApiAddress) {
      this.rootApiUrl = environment.rootApiAddress;
    }
  }

  generateQuote(employeeId: string, names: string[]): Observable<BenefitsCostQuote> {
    const url = this.rootApiUrl + '/quote/';

    const benefitsCostQuoteRequest = {
      employeeId: employeeId,
      dependentNames: names
    };

    return this.http.post<BenefitsCostQuote>(url, benefitsCostQuoteRequest, this.httpOptions);
  }
}
