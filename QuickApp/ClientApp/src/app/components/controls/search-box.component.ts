
import { Component, ViewChild, ElementRef, Input, Output, EventEmitter } from '@angular/core';

@Component({
    selector: 'search-box',
    templateUrl: './search-box.component.html',
    styleUrls: ['./search-box.component.scss']
})
export class SearchBoxComponent {
    searchKey: string;

    @Input()
    placeholder = 'Search...';

    @Output()
    searchChange = new EventEmitter<string>();

    // @ViewChild('searchInput', {static: false})
    // searchInput: ElementRef;


    onValueChange(value: string) {
        setTimeout(() => this.searchChange.emit(value));
    }


    clear() {

      this.searchKey = "";
        this.onValueChange(this.searchKey);
    }
}
