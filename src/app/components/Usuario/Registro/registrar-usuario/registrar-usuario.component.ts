import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { DadosRegistro } from 'src/app/models/DadosRegistro';
import { UsuariosService } from 'src/app/services/usuarios.service';

@Component({
  selector: 'app-registrar-usuario',
  templateUrl: './registrar-usuario.component.html',
  styleUrls: ['./registrar-usuario.component.css']
})
export class RegistrarUsuarioComponent {

  formulario: any;
  foto: File | null = null;
  erros: string[];


  constructor(private usuariosService: UsuariosService,
    private router: Router){ }

    ngOnInit(): void{
      this.erros = [];

      this.formulario = new FormGroup({
        nomeusuario: new FormControl(null, [Validators.required, Validators.minLength(6), Validators.maxLength(50)]),
        cpf: new FormControl(null, [Validators.required, Validators.minLength(1), Validators.maxLength(20)]),
        profissao: new FormControl(null,[Validators.required, Validators.minLength(1), Validators.maxLength(30)]),
        foto: new FormControl(null, [Validators.required]),
        email: new FormControl(null, [Validators.required, Validators.email, Validators.minLength(10), Validators.maxLength(50)]),
        senha: new FormControl(null, [Validators.required, Validators.minLength(6), Validators.maxLength(50)])
      });
    }

    get propriedade(){
      return this.formulario.controls;
    }

    SelecionarFoto(fileInput: any): void{
      this.foto = fileInput.target.files[0] as File;
      const reader = new FileReader();
      reader.onload = function(e : any){
        document.getElementById('foto')?.removeAttribute('hidden');
        //Onde estara a url da foto selecionada
        document.getElementById('foto')?.setAttribute('src', e.target.result)
      }
      //Ler foto como se fosse url
      reader.readAsDataURL(this.foto);
    }

    EnviarFormulario(): void{
      this.erros = [];
      const usuario = this.formulario.value;
      //Atraves do form data será enviado a foto para o backend
      const formData: FormData = new FormData();

      if(this.foto != null){
        formData.append('file', this.foto, this.foto.name);
      }

      this.usuariosService.SalvarFoto(formData).subscribe(resultado =>{
        const dadosRegistro : DadosRegistro = new DadosRegistro();
        dadosRegistro.nomeusuario = usuario.nomeusuario;
        dadosRegistro.cpf = usuario.cpf;
        dadosRegistro.foto = resultado.foto;
        dadosRegistro.profissao = usuario.profissao;
        dadosRegistro.email = usuario.email;
        dadosRegistro.senha = usuario.senha;

        this.usuariosService.RegistrarUsuario(dadosRegistro).subscribe(dados=>{
          const emailUsuarioLogado = dados.emailUsuarioLogado;
          const usuarioId = dados.usuarioId;
          const tokenUsuarioLogado = dados.tokenUsuarioLogado
          //Local storage o usuário tem no navegador dele, assim o email poderá ser usado em todo sistema
          localStorage.setItem('EmailUsuarioLogado', emailUsuarioLogado);
          localStorage.setItem('UsuarioId', usuarioId);
          localStorage.setItem('TokenUsuarioLogado', tokenUsuarioLogado)
          this.router.navigate(['categorias/listagemcategoria']);
        });
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
      });
    }
}

