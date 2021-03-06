import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuoteComponent } from './quote.component';
import {QuoteRoutingModule} from './quote-routing.module';

import {MatButtonModule, MatFormFieldModule, MatInputModule, MatCardModule, MatDividerModule, MatListModule} from '@angular/material';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {SharedModule} from '../shared/shared.module';
import {FormsModule} from '@angular/forms';

@NgModule({
  declarations: [
    QuoteComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    SharedModule,
    BrowserAnimationsModule,
    QuoteRoutingModule,
  ]
})
export class QuoteModule { }
