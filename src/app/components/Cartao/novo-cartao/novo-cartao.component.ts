import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { CartoesService } from 'src/app/services/cartoes.service';

@Component({
  selector: 'app-novo-cartao',
  templateUrl: './novo-cartao.component.html',
  styleUrls: ['../listagem-cartoes/listagem-cartoes.component.css']
})
export class NovoCartaoComponent implements OnInit {
  formulario: any;
  erros: string[];
  usuarioId: string = localStorage.getItem('UsuarioId') ?? '';
  constructor(private cartoesService : CartoesService,
    private router: Router,
    private snackBar: MatSnackBar){ }

  ngOnInit(): void {
      this.erros = [];

      this.formulario = new FormGroup({
        nome: new FormControl(null, [Validators.required, Validators.minLength(1), Validators.maxLength(20)]),
        bandeira: new FormControl(null, [Validators.required, Validators.minLength(1), Validators.maxLength(15)]),
        numero: new FormControl(null, [Validators.required, Validators.minLength(1), Validators.maxLength(20)]),
        limite: new FormControl(null, Validators.required),
        usuarioId: new FormControl(this.usuarioId)
      });
  }

  get propriedade(){
    return this.formulario.controls;
  }

  VoltarListagem(): void {
    this.router.navigate(['cartoes/listagemcartoes']);
  }

  EnviarFormulario(): void {
    this.erros = [];
    const cartao = this.formulario.value;

    this.cartoesService.NovoCartao(cartao).subscribe(resultado =>{
      this.router.navigate(['cartoes/listagemcartoes']);
      this.snackBar.open(resultado.mensagem, '', {
        duration: 2000,
        horizontalPosition: "right",
        verticalPosition: "top"
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
}
