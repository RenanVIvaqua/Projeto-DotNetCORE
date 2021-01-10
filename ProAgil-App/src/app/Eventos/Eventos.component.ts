import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-Eventos',
  templateUrl: './Eventos.component.html',
  styleUrls: ['./Eventos.component.css']
})
export class EventosComponent implements OnInit {

  eventos: any;
  
  getEvento(){
    this.http.get('http://localhost:5000/api/values').subscribe(
      response => {
        this.eventos = response;
      }, 
      error => {
        console.log(error);
      });
  }

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getEvento();
  }

}
