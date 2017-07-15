
//import { Component } from '@angular/core';
import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';


@Component({
    selector: 'weather',
    template: require('./weather.component.html')
})

export class WeatherComponent {
    public weather: Weather;

    constructor(private http: Http, @Inject('ORIGIN_URL') private originUrl: string) {
    }

    public getWeather(chosenCity: string) {
        this.http.get(this.originUrl + '/api/weather/city/' + chosenCity).subscribe(result => {
            this.weather = result.json()
        });
    }
}

interface Weather {
    temp: string;
    summary: string;
    city: string;
}