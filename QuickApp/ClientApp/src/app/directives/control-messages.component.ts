import {Component, Input} from '@angular/core';
import {FormGroup, FormControl} from '@angular/forms';
import { ValidationService } from '../services/validation.service';

@Component({
    selector: 'control-messages',
    template: `<div style="color:red" *ngIf="errorMessage !== null">{{errorMessage}}</div>`
})
export class ControlMessagesComponent {
   // errorMessage: string;

    @Input() control: FormControl;
    @Input() controltouched: boolean;
    constructor() { }


    get errorMessage() {
        //debugger;
        for (let propertyName in this.control.errors) {

            if (this.control.errors.hasOwnProperty(propertyName) && (this.control.touched || this.controltouched))
            {
                return ValidationService.getValidationErrorMessage(propertyName, this.control.errors[propertyName], this.control);
            }
        }

        return null;
    }

}
