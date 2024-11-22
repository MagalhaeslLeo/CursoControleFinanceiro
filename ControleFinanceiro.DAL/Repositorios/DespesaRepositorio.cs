using ControleFinanceiro.DAL.Interfaces;
using ControleFInanceiro.BLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL.Repositorios
{
  public class DespesaRepositorio : RepositorioGenerico<Despesa>, IDespesaRepositorio
  {
    private readonly Contexto _contexto;
    public DespesaRepositorio(Contexto contexto) : base(contexto)
    {
      _contexto= contexto;
    }

    public void ExcluirDespesas(IEnumerable<Despesa> despesas)
    {
      try
      {
        _contexto.Despesas.RemoveRange(despesas);
      }
      catch (Exception expection)
      {

        throw new Exception(expection.Message, expection);
      }
    }

    public IQueryable<Despesa> PegarDespesasPeloUsuarioId(string usuarioId)
    {
      try
      {
        return _contexto.Despesas.Include(d =>d.Cartao).
          Include(d=>d.Categoria).Where(d=>d.UsuarioID == usuarioId);
      }
      catch (Exception expection)
      {

        throw new Exception(expection.Message, expection);
      }
    }
  }
}
