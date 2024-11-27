using ControleFinanceiro.DAL.Interfaces;
using ControleFInanceiro.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL.Repositorios
{
  public class MesRepositorio : RepositorioGenerico<Mes>, IMesRepositorio
  {
    public MesRepositorio(Contexto contexto) : base(contexto)
    {
    }
  }
}
