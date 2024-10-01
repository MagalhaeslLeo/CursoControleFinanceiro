import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListagemCategoriaComponent } from './components/Categoria/listagem-categoria/listagem-categoria.component';
import { NovaCategoriaComponent } from './components/Categoria/nova-categoria/nova-categoria.component';
import { AtualizarCategoriaComponent } from './components/Categoria/atualizar-categoria/atualizar-categoria.component';
import { ListagemFuncoesComponent } from './components/Funcao/listagem-funcoes/listagem-funcoes.component';
import { NovaFuncaoComponent } from './components/Funcao/nova-funcao/nova-funcao.component';
const routes: Routes = [
  {
    path: 'categorias/listagemcategoria', component: ListagemCategoriaComponent
  },
  {
    path: 'categorias/novacategoria', component: NovaCategoriaComponent
  },
  {
    path: 'categorias/atualizarcategoria/:id', component: AtualizarCategoriaComponent
  },
  {
    path: 'funcoes/listagemfuncoes', component: ListagemFuncoesComponent
  },
  {
    path: 'funcoes/nova-funcao', component: NovaFuncaoComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
