import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Observable, startWith, map } from 'rxjs';
import { FuncoesService } from 'src/app/services/funcoes.service';

@Component({
  selector: 'app-listagem-funcoes',
  templateUrl: './listagem-funcoes.component.html',
  styleUrls: ['./listagem-funcoes.component.css']
})
export class ListagemFuncoesComponent implements OnInit {

funcoes = new MatTableDataSource<any>();
displayedColumns: string[];
autoCompleteInput = new FormControl();
opcoesFuncoes: string[] = [];
nomesFuncoes: Observable<string[]>

@ViewChild(MatPaginator, {static: true})
paginator: MatPaginator;

@ViewChild(MatSort, {static:true})
sort: MatSort;

  constructor(private funcoesService: FuncoesService){ }

  ngOnInit(): void {
      this.funcoesService.PegarTodos().subscribe(resultado => {
        resultado.forEach(funcao => {
          this.opcoesFuncoes.push(funcao.name)
        });

        this.funcoes.data = resultado;
        this.funcoes.sort = this.sort;
        this.funcoes.paginator = this.paginator;
      });

      this.displayedColumns = this.ExibirColunas();

      this.nomesFuncoes = this.autoCompleteInput.valueChanges.pipe
      (startWith(''), map(nome => this.FiltrarNomes(nome) ))
  }

  ExibirColunas(): string[]{
    return ['nome', 'descricao', 'acoes'];
  }

  FiltrarNomes(nome: string) : string[]{
    if(nome.trim().length >= 4){
      this.funcoesService.FiltrarFuncao(nome.toLowerCase()).subscribe(resultado=>{
        this.funcoes.data = resultado;
      });
    }
    else{
      if(nome === ''){
        this.funcoesService.PegarTodos().subscribe(resultado =>{
          this.funcoes.data = resultado;
        });
      }
    }

    return this.opcoesFuncoes.filter(funcao=> funcao.toLowerCase().includes(nome.toLowerCase()));
  }
}
