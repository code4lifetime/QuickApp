
import { NativeDateAdapter } from '@angular/material';
const SUPPORTS_INTL_API = typeof Intl !== 'undefined';

export class AppDateAdapter extends NativeDateAdapter {
    //useUtcForDisplay= true;
    format(date: Date, displayFormat: Object): string {
        if (displayFormat === 'input') {
            const day = date.getDate();
            const month = this.getMonthNames()[date.getMonth()];
            const year = date.getFullYear();
            return `${day}-${month}-${year}`;
        } else {
            return date.toDateString();
        }
    }

    getMonthNames(): string[] {

        const SHORT_NAMES = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];

        return SHORT_NAMES;
    }

    createDateAsUTC(date) {
        return new Date(Date.UTC(date.getFullYear(), date.getMonth(), date.getDate(), date.getHours(), date.getMinutes(), date.getSeconds()));
    }

    convertDateToUTC(date) {
        return new Date(date.getUTCFullYear(), date.getUTCMonth(), date.getUTCDate(), date.getUTCHours(), date.getUTCMinutes(), date.getUTCSeconds());
    }


}

export const APP_DATE_FORMATS =
    {
        parse: {
            dateInput: { month: 'short', year: 'numeric', day: 'numeric' },
        },
        display: {
            dateInput: 'input',
            monthYearLabel: { year: 'numeric', month: 'numeric' },
            dateA11yLabel: { year: 'numeric', month: 'long', day: 'numeric' },
            monthYearA11yLabel: { year: 'numeric', month: 'long' },
        }
    };