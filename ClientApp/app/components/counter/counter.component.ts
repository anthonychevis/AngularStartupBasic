import { Component } from '@angular/core';

@Component({
    selector: 'counter',
    templateUrl: './counter.component.html'
})
export class CounterComponent {
    public static GlobalCounter = 0;
    public currentCount = 0;

    public incrementCounter() {
        this.currentCount++;
        CounterComponent.GlobalCounter++;
    }

    public globalCount() {
        return CounterComponent.GlobalCounter;
    }
}
