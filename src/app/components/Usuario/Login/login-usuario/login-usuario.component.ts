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
        email: new FormControl(null, [Validators.required, Validators.email, Validators.minLength(10), Validators.maxLength(50)]),
        senha: new FormControl(null,[Validators.required, Validators.minLength(6), Validators.maxLength(50)])
      });
  }

  get propriedade(){
    return this.formulario.controls;
  }

  EnviarFormulario(): void{
    this.erros = [];
    const dadosLogin = this.formulario.value;

    this.usuariosService.LogarUsuario(dadosLogin).subscribe(resultado=>{
      const emailUsuarioLogado = resultado.emailUsuarioLogado;
      const usuarioId = resultado.usuarioId;
      localStorage.setItem("EmailUsuarioLogado", emailUsuarioLogado);
      localStorage.setItem("UsuarioId", usuarioId);
      this.router.navigate(['categorias/listagemcategoria']);
    }, (err)=>{
      if(err.status === 400){
        //Indo de campo em campo no objeto de erro que retorna do meu BadRequest
        //no meu backend
        for(const campo in err.error.errors){
          if(err.error.errors.hasOwnProperty(campo)){
            this.erros.push(err.error.errors[campo]);
          }
        }
      }
      else{
        this.erros.push(err.error);
      }
    });
  }
}
