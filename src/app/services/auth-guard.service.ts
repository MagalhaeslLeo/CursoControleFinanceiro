import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import decode from 'jwt-decode';

interface MyToken {
  exp: number;
  iat: number;
  nbf: number;
  role: string;
  unique_name: string;
}

@Injectable({
  providedIn: 'root'
})
//CanActivate para proteger as rotas
export class AuthGuardService implements CanActivate {

  //JwtHelper para verificar se o token já expirou
  constructor(private jwtHelper: JwtHelperService,
    private router: Router
  ) { }


  canActivate(): boolean{
    const token = localStorage.getItem('TokenUsuarioLogado');
    //Se o token existir e não esteja expirado, continuar navegando
    //Caso contrário, enviar usuário para a tela de login
    if(token && !this.jwtHelper.isTokenExpired(token)){
      return true;
    }

    this.router.navigate(['usuarios/loginusuario']);
    return false;
  }

   VerificarAdministrador() : boolean{
    const token = localStorage.getItem('TokenUsuarioLogado');
    if(token) {
      const tokenUsuario = decode<MyToken>(token)
    if(tokenUsuario.role === 'Administrador'){
      return true;
    }
  }
      return false;
  }
}
