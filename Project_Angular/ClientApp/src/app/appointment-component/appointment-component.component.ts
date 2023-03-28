import { Component, OnInit } from '@angular/core';
import {
  HttpClientModule,
  HttpClient,
  HttpParams,
  HttpHeaders
} from '@angular/common/http';
@Component({
  selector: 'app-appointment-component',
  templateUrl: './appointment-component.component.html',
  styleUrls: ['./appointment-component.component.css']
})
export class AppointmentComponentComponent implements OnInit {


  ngOnInit(): void {
  }
  public files: any[];
  items: any;
  constructor(public http: HttpClient) {
    this.files = [];
    this.http.get('https://localhost:7179/AppointmentService/GetAllAppointment')
      .subscribe(data => {
        this.items = data;
      });
  }

}
