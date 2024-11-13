import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { CartoesService } from 'src/app/services/cartoes.service';

@Component({
  selector: 'app-listagem-cartoes',
  templateUrl: './listagem-cartoes.component.html',
  styleUrls: ['./listagem-cartoes.component.css']
})
export class ListagemCartoesComponent implements OnInit {

  cartoes = new MatTableDataSource<any>();
  displayedColumns: string[];
  usuarioId: string = localStorage.getItem('UsuarioId') ?? '';

  @ViewChild(MatPaginator, {static: true})
  paginator: MatPaginator;

  @ViewChild(MatSort, {static:true})
  sort: MatSort;

  constructor(private cartoesService: CartoesService, private dialog: MatDialog){}

  ngOnInit(): void {
      this.cartoesService.PegarCartoesPeloUsuarioId(this.usuarioId).subscribe(resultado =>{
        this.cartoes.data = resultado;
        this.cartoes.paginator = this.paginator;
        this.cartoes.sort = this.sort;
      });
      
      this.displayedColumns = this.ExibirColunas();
  }

  ExibirColunas(): string[]{
        return ['nome', 'bandeira', 'numero', 'limite', 'acoes'];
  }

  AbrirDialog(cartaoId: number, nome: string): void{
    
  }
}

@Component({
  selector: 'app-dialog-exclusao-cartao',
  templateUrl: 'dialog-exclusao-cartao.html'
})

export class DialogExclusaoCartaoComponent{
  constructor(@Inject(MAT_DIALOG_DATA) public dados: any,
  private cartoesService: CartoesService,
  private snackBar: MatSnackBar
){ }

  ExcluirCartao(cartaoId : number): void{
    this.cartoesService.ExcluirCartao(cartaoId).subscribe(resultado=>{
      this.snackBar.open(resultado.mensagem, '', {
        duration: 2000,
        horizontalPosition: 'right',
        verticalPosition: 'top'
      });
    });
  }
}