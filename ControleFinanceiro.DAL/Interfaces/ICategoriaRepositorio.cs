using ControleFInanceiro.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL.Interfaces
{
    public interface ICategoriaRepositorio : IRepositorioGenerico<Categoria>
    {
        //Sobreescrevendo metodos para categorias terem seus tipos relacionados
        new IQueryable<Categoria> PegarTodos();
        new Task<Categoria> PegarPeloID(int id);
        IQueryable<Categoria> FiltrarCategorias(string nomeCategoria);
    }
}
