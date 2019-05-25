import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {QuoteComponent} from './quote.component';

const routes: Routes = [
  {
    path: 'quote/employee/:employeeId',
    component: QuoteComponent,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class QuoteRoutingModule {
  static routes: Routes = routes;
}
