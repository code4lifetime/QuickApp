import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import * as Material from '@angular/material';

// import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
// import {
//   MatSnackBarModule,
//   MatTableModule,
//   MatTooltipModule,
//   MatDatepickerModule,
//   MatNativeDateModule,
//   MatSelectModule,
//   MAT_DATE_FORMATS,
//   MatFormFieldModule,
//   MatIconModule,
//   MatRadioModule,
//   MatPaginatorModule,
//   MatInputModule,
//   MatButtonModule,
//   MatCheckboxModule,
//   MatSortModule,
//   MatDialogModule,
//   DateAdapter
// } from '@angular/material';


import { CdkTableModule } from '@angular/cdk/table';
import { ControlMessagesComponent } from './directives/control-messages.component';
import { AppDateAdapter, APP_DATE_FORMATS } from './directives/date-format';


@NgModule({
  declarations: [
    ControlMessagesComponent
  ],
  imports: [
    CommonModule,
    Material.MatTabsModule,
    Material.MatTableModule,
    Material.MatPaginatorModule,
    Material.MatFormFieldModule,
    Material.MatButtonModule,
    Material.MatCheckboxModule,
    Material.MatInputModule,
    Material.MatIconModule,
    Material.MatDialogModule,
    Material.MatRadioModule,
    Material.MatDatepickerModule,
    Material.MatNativeDateModule,
    Material.MatSelectModule,
    Material.MatTooltipModule,
    Material.MatProgressSpinnerModule,
    Material.MatSortModule,
    Material.MatSnackBarModule
  ],
  exports: [
    CommonModule,
    Material.MatTabsModule,
    ControlMessagesComponent,
    Material.MatTableModule,
    Material.MatPaginatorModule,
    Material.MatSortModule,
    Material.MatFormFieldModule,
    Material.MatButtonModule,
    Material.MatCheckboxModule,
    Material.MatInputModule,
    Material.MatIconModule,
    Material.MatDialogModule,
    Material.MatRadioModule,
    Material.MatDatepickerModule,
    Material.MatNativeDateModule,
    Material.MatSelectModule,
    Material.MatTooltipModule,
    Material.MatProgressSpinnerModule
  ],
  providers: [{
    provide: Material.DateAdapter, useClass: AppDateAdapter
},
{
    provide: Material.MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS
}]
})
export class MaterialModule { }
