using ControleFinanceiro.DAL.Interfaces;
using ControleFInanceiro.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL.Repositorios
{
  public class CartaoRepositorio : RepositorioGenerico<Cartao>, ICartaoRepositorio
  {
    private readonly Contexto _contexto;
    public CartaoRepositorio(Contexto contexto) : base(contexto)
    {
      _contexto = contexto;
    }

    public IQueryable<Cartao> PegarCartoesPeloUsuarioId(string usuarioId)
    {
      try
      {
        return _contexto.Cartoes.Where(c=>c.UsuarioID == usuarioId);
      }
      catch (Exception expection)
      {

        throw new Exception(expection.Message, expection);
      }
    }
  }
}
