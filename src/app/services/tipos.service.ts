import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Tipo } from '../models/Tipo';

@Injectable({
  providedIn: 'root'
})
export class TiposService {

  url: string = 'api/Tipos'

  constructor(private http: HttpClient) { }
//Observable = coleção de itens que exibe notificações quando os itens são mudados
//Coleção de tipos vindos da url
//O http.get faz aciona a requisição get do backend, assim ele sabe qual endpoint atingir
  PegarTodos(): Observable<Tipo[]>{
    return this.http.get<Tipo[]>(this.url);
  }

}
