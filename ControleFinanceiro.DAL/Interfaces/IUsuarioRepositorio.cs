using ControleFInanceiro.BLL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL.Interfaces
{
  public interface IUsuarioRepositorio : IRepositorioGenerico<Usuario>
  {
    Task<int> PegarQuantidadeUsuariosRegistrados();
    Task<IdentityResult> CriarUsuario(Usuario usuario, string senha);
    Task IncluirUsuarioEmFuncao(Usuario usuario, string funcao);
    Task LogarUsuario(Usuario usuario, bool lembrar);
    Task<Usuario> PegarUsuarioPeloEmail(string email);
    Task<IList<string>> PegarFuncoesUsuario(Usuario usuario);
  }
}
