//import { Component } from '@angular/core';

//@Component({
//    selector: 'app-bar-chart',
//    templateUrl: './bar-chart.component.html'
//})
///** bar-chart component*/
//export class BarChartComponent {    
    
//}

import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
  selector: 'app-bar-chart',
  templateUrl: './bar-chart.component.html',
})


export class BarChartComponent {
  public weatherForecast: WeatherForecast;
  public chartLegend: boolean = true;
  public chartType: string = 'bar';


  public chartOptions: any = {
    responsive: true,
    legend: {
      position: 'bottom'
    }
  };

  constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
    http.get(baseUrl + 'api/LineChart/GetWeatherForecast').subscribe(result => {
      this.weatherForecast = result.json() as WeatherForecast;
    }, error => console.error(error));
  }
}


interface Weather {
  data: Array<number>;
  label: string;
}

interface WeatherForecast {
  weatherList: Weather[];
  chartLabels: string[];
}  
