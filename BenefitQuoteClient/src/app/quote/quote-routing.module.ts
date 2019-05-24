import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {QuoteComponent} from './quote.component';

const routes: Routes = [
  {
    path: 'quote',
    component: QuoteComponent,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class QuoteRoutingModule { }
