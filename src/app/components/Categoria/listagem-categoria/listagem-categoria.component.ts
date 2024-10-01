import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { CategoriasService } from 'src/app/services/categorias.service';
import { MatTableDataSource } from '@angular/material/table';
import { MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { FormControl } from '@angular/forms';
import { Observable, startWith, map } from 'rxjs';
import { MatPaginator } from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-listagem-categoria',
  templateUrl: './listagem-categoria.component.html',
  styleUrls: ['./listagem-categoria.component.css']
})
export class ListagemCategoriaComponent implements OnInit {

  categorias = new MatTableDataSource<any>();
  displayedColumns: string[];
  autoCompleteInput = new FormControl();
  //Array de string responsável por ter os nomes das categorias
  opcoesCategoria : string[] = [];
  //Responsavel por mostrar a lista de categorias que tem para o usuário
  nomesCategorias : Observable<string[]>;
  //Inicializando o serviço via injeção de dependência
  @ViewChild(MatPaginator, {static: true})
  paginator: MatPaginator
  @ViewChild(MatSort, {static:true} )
  sort: MatSort;

  constructor(private categoriasService: CategoriasService,
    private dialog: MatDialog) { }
  //Ao iniciar pegar todos os atributos, peço para executar através do subscribe
  ngOnInit(): void {
    this.categoriasService.PegarTodos().subscribe(resultado =>{
      resultado.forEach(categoria =>{
        this.opcoesCategoria.push(categoria.nome);
      });
      this.categorias.data = resultado;
      this.categorias.paginator = this.paginator;
      this.categorias.sort = this.sort;
    });

    this.displayedColumns = this.ExibirColunas();
    //Valores filtrados do autocomplete
    //pipe = função de transformação de dados
    //Texto sempre começará com uma string vazia
    this.nomesCategorias = this.autoCompleteInput.valueChanges.pipe(startWith('')
    , map(nome => this.FiltrarNomes(nome)));
  }
  ExibirColunas() : string[]{
    return ['nome', 'icone', 'tipo', 'acoes' ]
  }

  AbrirDialog(categoriaID : number, nome : string) : void{
    this.dialog.open(DialogExclusaoCategoriaComponent, {
      data: {
        categoriaID : categoriaID,
        nome: nome
      }
    }).afterClosed().subscribe(resultado =>{
      if(resultado === true){
        this.categoriasService.PegarTodos().subscribe(dados =>{
          this.categorias.data = dados;
        });

        this.displayedColumns = this.ExibirColunas();
      }
    });
  }
  //Efetuar chamadas no banco de dados se o nome for maior que 4 caracteres
  FiltrarNomes(nome: string) : string[]{
    if(nome.trim().length >= 4){
      this.categoriasService.FiltrarCategorias(nome.toLowerCase()).subscribe(resultado =>{
        this.categorias.data = resultado;
      });
    }
    else{
      if(nome === ''){
        this.categoriasService.PegarTodos().subscribe(resultado =>{
          this.categorias.data = resultado;
        });
      }
    }
    //Comparando os valores de categoria com o que está indo de nome
    return this.opcoesCategoria.filter(categoria =>
      //includes retornará true se nome for uma substring de categoria
      categoria.toLowerCase().includes(nome.toLowerCase())
    );
  }

}

@Component({
  selector: 'app-dialog-exclusao-categoria',
  templateUrl: 'dialog-exclusao-categoria.html'
})
export class DialogExclusaoCategoriaComponent{
  //MatDialogData permite enviar dados de listagem categoria para o dialog
  constructor(@Inject(MAT_DIALOG_DATA) public dados : any,
  private categoriasService: CategoriasService,
  private snackBar: MatSnackBar
){ }
  
  ExcluirCategoria(categoriaID : number) : void{
    this.categoriasService.ExcluirCategoria(categoriaID).subscribe(resultado =>{
      this.snackBar.open(resultado.mensagem, '', {
        duration: 2000,
        horizontalPosition: 'right',
        verticalPosition: 'top'
      });
    });
  }
}