import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Evento } from '../_models/Evento';
import { EventoService } from '../_service/evento.service';

@Component({
  selector: 'app-Eventos',
  templateUrl: './Eventos.component.html',
  styleUrls: ['./Eventos.component.css']
})
export class EventosComponent implements OnInit { 
  eventosFiltrados!: Evento[];
  eventos!: Evento[];
  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;
  modalRef!: BsModalRef;
  
  constructor( private eventoService: EventoService 
    ,private modalService: BsModalService
    ) {}
    
    _filtroLista!: string;
    get filtroLista(): string
    {
      return this._filtroLista;
    }
    set filtroLista(values: string)
    {
      this._filtroLista = values;
      this.eventosFiltrados = this.filtroLista ? this.filtrarEvento(this.filtroLista) : this.eventos;
    }
    
    ngOnInit() {
      this.getEvento();
    }
    
    filtrarEvento(filtrarPor: string): Evento[]
    {
      filtrarPor = filtrarPor.toLocaleLowerCase();        
      return this.eventos.filter(evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1);
    }
    
    getEvento()
    {
      this.eventoService.getAllEvento().subscribe(
        (_eventos: Evento[]) =>
        {
          this.eventos = _eventos;
          this.eventosFiltrados = this.eventos;
        },
        error => {
          console.log(error);
        });
      }
      
      AlterarImagem()
      {
        this.mostrarImagem = !this.mostrarImagem;
      }  
      
      OpenModal(Template: TemplateRef<any>)
      {
        this.modalRef = this.modalService.show(Template);
      }
    }
    