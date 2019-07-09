import {Directive, ElementRef, HostListener, Input, Renderer} from '@angular/core';


// numeric field Directive (accept only numerics)
@Directive({
    selector: '[numeric-field]'
})

export class NumericFieldDirective {
    @HostListener('keypress', ['$event']) onKeyPress(e: any) {
        var regex = new RegExp("^[0-9\b]+$");

        if (e.charCode == 0) {
            return true;
        }
        else {
            var key = String.fromCharCode(!e.charCode ? e.which : e.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        }
    }
}


// Character field Directive (accept only characters)
@Directive({
    selector: '[character-field]'
})
export class CharacterFieldDirective {
    @HostListener('keypress', ['$event']) onKeyPress(e: any) {
        var regex = new RegExp("^[a-zA-Z\b]+$");

        if (e.charCode == 0) {
            return true;
        }
        else {
            var key = String.fromCharCode(!e.charCode ? e.which : e.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        }


    }


}

@Directive({
    selector: '[alfanumeric-field]'
})

export class AlfaNumericFieldDirective {
    @HostListener('keypress', ['$event']) onKeyPress(e: any) {
        debugger;
        //var regex = new RegExp("^\[1-9]+(.[1-9]{1,2})?$");
        var regex = new RegExp("^[a-zA-Z0-9\b]+$");

        if (e.charCode == 0) {
            return true;
        }
        else {
            var key = String.fromCharCode(!e.charCode ? e.which : e.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        }
    }
}

// currency field Directive (accept only decimal)
@Directive({
    selector: '[amount-field]'
})

export class AmountFieldDirective {
    @HostListener('keypress', ['$event']) onKeyPress(e: any) {
        debugger;
        //var regex = new RegExp("^\[1-9]+(.[1-9]{1,2})?$");
        var regex = new RegExp("/^\d+(\.\d{1,2})?$/");

        if (e.charCode == 0) {
            return true;
        }
        else {
            var key = String.fromCharCode(!e.charCode ? e.which : e.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        }
    }
}
