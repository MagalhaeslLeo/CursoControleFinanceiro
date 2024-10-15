import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UsuariosService } from 'src/app/services/usuarios.service';

@Component({
  selector: 'app-login-usuario',
  templateUrl: './login-usuario.component.html',
  styleUrls: ['./login-usuario.component.css']
})
export class LoginUsuarioComponent implements OnInit {

  formulario: any;
  erros: string[];

  constructor(private usuariosService: UsuariosService,
    private router: Router
  ){}

  ngOnInit(): void {
      this.erros = [];

      this.formulario = new FormGroup({
        email: new FormControl(null, [Validators.required, Validators.minLength(10), Validators.maxLength(50)]),
        senha: new FormControl(null,[Validators.required, Validators.minLength(6), Validators.maxLength(50)])
      });
  }

  get propriedade(){
    return this.formulario.controls;
  }
}
