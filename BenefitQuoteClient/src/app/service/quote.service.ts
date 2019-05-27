import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class QuoteService {
  private rootApiUrl: string = '';

  constructor(private http: HttpClient) {
    if (environment.rootApiAddress) {
      this.rootApiUrl = environment.rootApiAddress;
    }
  }

  generateQuote(employeeId: string, names: string[]): Observable<Quote> {
    throw new Error('Method not implemented.');
  }
}
