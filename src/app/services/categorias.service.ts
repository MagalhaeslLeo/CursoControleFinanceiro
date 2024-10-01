import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Categoria } from '../models/Categoria';

//Configuração do cabeçalho da requisição http, pois serão enviados dados para a API
const httpOptions = {
  headers: new HttpHeaders ({
    'Content-Type' : 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class CategoriasService {

  url = 'api/Categorias';

  constructor(private http: HttpClient) { }

  PegarTodos(): Observable<Categoria[]> {
    return this.http.get<Categoria[]>(this.url);
  }

  PegarCategoriaPeloID(categoriaID: number) : Observable<Categoria>{
    const apiUrl = `${this.url}/${categoriaID}`;
    return this.http.get<Categoria>(apiUrl);
  }

  //any pois o tipo de dado não vai ser definido aqui, permite que seja definido em tempo de execução
  //No retorno preciso passar a categoria e as opções do cabeçalho para envio de dados
  NovaCategoria(categoria : Categoria) : Observable<any>{
    return this.http.post<Categoria>(this.url, categoria, httpOptions);
  }
  //Quando estou enviando um ID preciso redefinir a URL
  AtualizarCategoria(categoriaID: number, categoria: Categoria): Observable<any>{
    const apiUrl = `${this.url}/${categoriaID}`;
    return this.http.put<Categoria>(apiUrl, categoria, httpOptions);
  }
  //Nesse caso retorno um número para dizer qual categoria vai ser excluída
  ExcluirCategoria(categoriaID: number) : Observable<any>{
    const apiUrl = `${this.url}/${categoriaID}`;
    return this.http.delete<number>(apiUrl, httpOptions);
  }

  FiltrarCategorias(nomeCategoria: string) : Observable<Categoria[]>{
    const apiUrl = `${this.url}/FiltrarCategorias/${nomeCategoria}`;
    return this.http.get<Categoria[]>(apiUrl)
  }
}
