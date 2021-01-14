import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-Eventos',
  templateUrl: './Eventos.component.html',
  styleUrls: ['./Eventos.component.css']
})
export class EventosComponent implements OnInit {

  _filtroLista: string = "";
  get filtroLista(): string{
    return this._filtroLista;
  }
  set filtroLista(values: string){
    this._filtroLista = values;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEvento(this.filtroLista) : this.eventos;  
  }

  filtrarEvento(filtrarPor:string):any{
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1)
  }

  eventosFiltrados: any = [];
  eventos: any = [];
  imagemLargura = 50;
  imagemMargem =2;
  mostrarImagem = false;

  getEvento(){
    this.http.get('http://localhost:5000/api/values').subscribe(
      response => {
        this.eventos = response;
      }, 
      error => {
        console.log(error);
      });
  }

  AlterarImagem(){
    this.mostrarImagem = !this.mostrarImagem;
  }

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getEvento();
  }

}
