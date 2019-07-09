import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {
  MatSnackBarModule,
  MatTableModule,
  MatTooltipModule,
  MatDatepickerModule,
  MatNativeDateModule,
  MatSelectModule,
  MAT_DATE_FORMATS,
  MatFormFieldModule,
  MatIconModule,
  MatRadioModule,
  MatPaginatorModule,
  MatInputModule,
  MatButtonModule,
  MatCheckboxModule,
  MatSortModule,
  MatDialogModule,
  DateAdapter
} from '@angular/material';
import { CdkTableModule } from '@angular/cdk/table';
import { ControlMessagesComponent } from './directives/control-messages.component';
import { AppDateAdapter, APP_DATE_FORMATS } from './directives/date-format';

 //import { ControlMessagesComponent } from './shared/_directives/control-messages.component';
 //import { AppDateAdapter, APP_DATE_FORMATS } from './shared/_directives/date-format';


@NgModule({
  declarations: [
    ControlMessagesComponent
  ],
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatFormFieldModule,
    MatButtonModule,
    MatCheckboxModule,
    MatInputModule,
    MatIconModule,
    MatDialogModule,
    MatRadioModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule,
    MatTooltipModule,
    MatProgressSpinnerModule,
    MatSnackBarModule
  ],
  exports: [
    CommonModule,
    ControlMessagesComponent,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatFormFieldModule,
    MatButtonModule,
    MatCheckboxModule,
    MatInputModule,
    MatIconModule,
    MatDialogModule,
    MatRadioModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule,
    MatTooltipModule,
    MatProgressSpinnerModule
  ],
  providers: [{
    provide: DateAdapter, useClass: AppDateAdapter
},
{
    provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS
}]
})
export class MaterialModule { }
