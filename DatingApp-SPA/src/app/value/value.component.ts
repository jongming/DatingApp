import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-value',
  templateUrl: './value.component.html',
  styleUrls: ['./value.component.css']
})

export class ValueComponent implements OnInit { // OnInit has several steps it runs, constructor is first
  values: any;

  constructor(private http: HttpClient ) { } // inject httpClient service, does not call

  ngOnInit() { // Call API
    console.log('*****value.components.ts ngOnInit() ' + Date().toString());
    this.getValues();
  }

  getValues() {
    // http.get returns an observable JSON object and we must subscribe to it to use it.
    console.log('*****value.components.ts getValues() '  + Date().toString());
    this.http.get('http://localhost:5000/api/values').subscribe(response => {
      this.values = response;
    } , error => {
      console.log(error);
    });
  }

}
