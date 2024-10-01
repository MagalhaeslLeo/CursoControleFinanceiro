import { Component, OnInit } from '@angular/core';
import { Tipo } from 'src/app/models/Tipo';
import { TiposService } from 'src/app/services/tipos.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { CategoriasService } from 'src/app/services/categorias.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-nova-categoria',
  templateUrl: './nova-categoria.component.html',
  styleUrls: ['../listagem-categoria/listagem-categoria.component.css']
})
export class NovaCategoriaComponent {

  formulario: any;
  tipos: Tipo[];
  erros: string[];
  constructor( private tiposServices: TiposService,
    private categoriasService : CategoriasService,
    private router : Router,
    private snackBar: MatSnackBar
   ){

  }
  //Carregar os dados dos tipos que podem ser escolhidos
  ngOnInit() : void{
    this.erros = [];
    this.tiposServices.PegarTodos().subscribe(resultado => {
      this.tipos = resultado;
    });

    this.formulario = new FormGroup({
      nome: new FormControl(null, [Validators.required, Validators.maxLength(50)]),
      icone: new FormControl(null, [Validators.required, Validators.maxLength(15)]),
      tipoID: new FormControl(null, [Validators.required])
    });
  }
  //Lá no HTML vai bastar eu colocar propriedade.nomeDoAtributo
  get propriedade(){
    return this.formulario.controls;
  }

  EnviarFormulario(): void{
    const categoria = this.formulario.value;
    this.erros = [];
    this.categoriasService.NovaCategoria(categoria).subscribe(resultado => {
      this.router.navigate(['categorias/listagemcategoria']);
      this.snackBar.open(resultado.mensagem, '', {
        duration: 2000,
        horizontalPosition: 'right',
        verticalPosition: 'top'
      });
    },
      (err)=>{
        if(err.status === 400){
          //Indo de campo em campo no objeto de erro que retorna do meu BadRequest
          //no meu backend
          for(const campo in err.error.errors){
            if(err.error.errors.hasOwnProperty(campo)){
              this.erros.push(err.error.errors[campo]);
            }
          }
        }
      }
  );
  }

  VoltarListagem() : void{
    this.router.navigate(['categorias/listagemcategoria']);
  }
}
