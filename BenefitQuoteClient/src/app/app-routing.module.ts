import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';
import {HomeComponent} from './home/home.component';

const routes: Routes = [
  {
    path: 'quote',
    loadChildren: './quote/quote.module#QuoteModule'
  },
  {
    path: 'admin',
    loadChildren: './benefit-admin/benefit-admin.module#BenefitAdminModule'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    //preloadingStrategy: PreloadAllModules
  })],
  exports: [RouterModule]
})
export class AppRoutingModule {}
