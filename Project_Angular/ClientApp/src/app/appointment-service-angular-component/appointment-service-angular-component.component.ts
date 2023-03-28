import { Component, OnInit } from '@angular/core';

import {
  HttpClientModule,
  HttpClient,
  HttpParams,
  HttpHeaders
} from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { DataService } from "../Data.Service";
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-appointment-service-angular-component',
  templateUrl: './appointment-service-angular-component.component.html',
  styleUrls: ['./appointment-service-angular-component.component.css'],
  providers: [DataService]
})
export class AppointmentServiceAngularComponentComponent implements OnInit {

  ngOnInit() {


    this.data.user
      .subscribe(res => {
        if (!!res) {
          this.data2 = res;
        }
      });

  }
  public files: any[];
  items: any;
  sl: number = 0;
  name2: string = "";
  Appointment_Name2: string = "";
  Service_ID2: string = "";
  Service_Name2: string = '';
  Appointment_ID2: string = "";
  Service_Fee2: number = 0;
  Date2: string = "";
  Phone2: string = "";
  Picture2: string = "";
  data2: string = "";
  message: string = "";

  constructor(public http: HttpClient, public data: DataService, private route: ActivatedRoute) {
    this.files = [];
    this.http.get('https://localhost:7179/AppointmentService/GetAllService')
      .subscribe(data => {
        this.items = data;
        console.log(this.items);
      });
    this.sl = 0;

    this.route.queryParams.subscribe(params => {
      this.Appointment_ID2 = params['Appointment_ID'];
      this.Appointmentchange();
    });


  }
  Appointmentchange() {
    this.items = [];
    this.Appointment_Name2 = "";
    this.Date2 = "";
    this.http.get('https://localhost:7179/AppointmentService/GetAppointment/' + this.Appointment_ID2)
      .subscribe(data => {
        if (data != "") {
          this.Appointment_Name2 = Object.values(data)[0].Appointment_Name;
          this.Date2 = Object.values(data)[0].Date;
          this.Phone2 = Object.values(data)[0].Phone;
          this.showItems();
        }
      });
  }
  showItems() {
    this.http.get('https://localhost:7179/AppointmentService/GetIService/' + this.Appointment_ID2)
      .subscribe(data => {
        this.items = data;
        console.log(this.items);
      });
    this.sl = 0;
  }
  onFileChanged(event: any) {
    this.files = event.target.files;
    const formData = new FormData();
    formData.append('files', this.files[0]);
    this.http.post('https://localhost:7179/AppointmentService/Post/', formData).subscribe(data => {
      this.Picture2 = this.files[0].name
    });
  }
  addItems(Appointment_Name: string, Service_ID: string, Service_Name: string, Appointment_ID: string, Service_Fee: string, Date: string, Phone: string, Picture: string): void {
    this.items.push({

      Service_ID: Service_ID,
      Service_Name: Service_Name,
      Service_Fee: Service_Fee,
      Picture: this.files[0].name,

    });

    this.Service_ID2 = '';
    this.Service_Name2 = "";
    this.Service_Fee2 = 0;

  }
  convertDate(inputFormat: Date) {
    function pad(s: number) { return (s < 10) ? '0' + s : s; }
    var d = new Date(inputFormat)
    return [d.getFullYear(), pad(d.getMonth() + 1), pad(d.getDate())].join('-')
  }
  show(Appointment_Name1: string, Service_ID1: string, Service_Name1: string, Appointment_ID1: string, Service_Fee1: number, Date1: string, Phone1: string, Picture1: string): void {

    this.Service_ID2 = Service_ID1;
    this.Service_Name2 = Service_Name1;
    this.Service_Fee2 = Service_Fee1;
    this.Picture2 = Picture1;

  }
  upDateService(Appointment_Name: HTMLInputElement, Service_ID: HTMLInputElement, Service_Name: HTMLInputElement, Appointment_ID: HTMLInputElement, Service_Fee: HTMLInputElement, Date: HTMLInputElement, Phone: HTMLInputElement): void {
    this.items[this.sl].Appointment_Name = Appointment_Name.value;
    this.items[this.sl].Service_ID = Service_ID.value;
    this.items[this.sl].Service_Name = Service_Name.value;
    this.items[this.sl].Service_Fee = Service_Fee.value;
    this.items[this.sl].Date = Date.value;
    this.items[this.sl].Phone = Phone.value;

    this.Service_ID2 = '';
    this.Service_Name2 = "";
    this.Service_Fee2 = 0;



  }
  deleteItems(): void {
    this.items.splice(this.sl, 1);
    this.Service_ID2 = '';
    this.Service_Name2 = "";
    this.Service_Fee2 = 0;
    this.Picture2 = "";

  }

  deleteAll(): void {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }
    var data = {};
    this.http.post<any>('https://localhost:7179/AppointmentService/RemoveAppointmentServiceVm/' + this.Appointment_ID2, JSON.stringify(data), httpOptions).subscribe(data => {
      window.location.href = 'https://localhost:4200/';
    });
  }

  saveAll(): void {

    var i = 0;
    var detailsArr = [];
    var Appointment = {
      Appointment_ID: this.Appointment_ID2,
      Appointment_Name: this.Appointment_Name2,
      Date: this.Date2,
      Phone: this.Phone2,

    };
    for (let value of this.items) {
      detailsArr.push({
        Service_ID: value.Service_ID,
        Service_Name: value.Service_Name,
        Appointment_ID: this.Appointment_ID2,
        Service_Fee: value.Service_Fee,
        Date: value.Date,
        Picture: value.Picture,
      });
    }
    var data = {
      "Appointment": Appointment,
      "Service": detailsArr
    };
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }
    console.log(JSON.stringify(data));
    this.http.post<any>('https://localhost:7179/AppointmentService/AddAppointmentServiceVm', JSON.stringify(data), httpOptions).subscribe(data => {
      window.location.href = 'https://localhost:4200/';
    });
  }
  myalert(data: string) {
    this.data.user
      .subscribe(res => {
        if (!!res) {
          this.data2 = res;
        }
      });
  }
}
