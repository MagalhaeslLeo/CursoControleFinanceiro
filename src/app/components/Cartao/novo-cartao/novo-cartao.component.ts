import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { CartoesService } from 'src/app/services/cartoes.service';

@Component({
  selector: 'app-novo-cartao',
  templateUrl: './novo-cartao.component.html',
  styleUrls: ['./novo-cartao.component.css']
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
}
