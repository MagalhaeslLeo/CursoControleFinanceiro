import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Funcao } from '../models/Funcao';
import { Observable } from 'rxjs';


const HttpOptions = {
  headers : new HttpHeaders({
    'Content-Type' : 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class FuncoesService {
url = "api/Funcoes";
  constructor(private http: HttpClient) { }

  PegarTodos(): Observable<Funcao[]>{
    return this.http.get<Funcao[]>(this.url);
  }

  PegarPeloId(funcaoID: string): Observable<Funcao>{
    const apiUrl = `${this.url}/${funcaoID}`;
    return this.http.get<Funcao>(apiUrl);
  }

  NovaFuncao(funcao: Funcao) : Observable<any>{
    return this.http.post<Funcao>(this.url, funcao, HttpOptions);
  }

  AtualizacaoFuncao(funcaoID: string, funcao: Funcao): Observable<any>{
    const apiUrl = `${this.url}/${funcaoID}`;
    return this.http.put<Funcao>(apiUrl, funcao, HttpOptions);
  }

  ExcluirFuncao(funcaoID: string): Observable<any>{
    const apiUrl = `${this.url}/${funcaoID}`;
    return this.http.delete<string>(apiUrl, HttpOptions);
  }

  FiltrarFuncao(nomeFuncao: string): Observable<Funcao[]>{
    const apiUrl = `${this.url}/FiltrarFuncoes/${nomeFuncao}`;
    return this.http.get<Funcao[]>(apiUrl);
  }
}
