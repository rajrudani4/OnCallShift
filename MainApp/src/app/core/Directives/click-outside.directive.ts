import { Directive, ElementRef, HostListener, Output, EventEmitter } from '@angular/core';

@Directive({ selector: '[clickOutside]' })
export class ClickOutsideDirective {

    constructor(private element : ElementRef) { }

    @Output() public clickOutside = new EventEmitter();

    @HostListener('document:click', ['$event.target'])
    public onClick(target : any) {
        const clickedInside = this.element.nativeElement.contains(target);
        if(!clickedInside) {
            this.clickOutside.emit(null);
        }
    }
}