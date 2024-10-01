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
    public class CategoriaRepositorio : RepositorioGenerico<Categoria>, ICategoriaRepositorio
    {
        private readonly Contexto _contexto;
        public CategoriaRepositorio(Contexto contexto) : base(contexto) 
        {
            _contexto = contexto;
        }

        public new IQueryable<Categoria> PegarTodos()
        {
            try
            {
                return _contexto.Categorias.Include(c => c.Tipo);
            }
            catch (Exception expection)
            {

                throw new Exception(expection.Message, expection);
            }
        }
        public new async Task<Categoria> PegarPeloID(int id)
        {
            try
            {
                var entity = await _contexto.Categorias.Include(c => c.Tipo)
                    .FirstOrDefaultAsync(c=>c.CategoriaID == id);
                return entity;
            }
            catch (Exception expection)
            {

                throw new Exception(expection.Message, expection);
            }
        }

        public IQueryable<Categoria> FiltrarCategorias(string nomeCategoria)
        {
            try
            {
                var entity = _contexto.Categorias.Include(c => c.Tipo)
                    .Where(c => c.Nome.Contains(nomeCategoria));
                return entity;
            }
            catch (Exception expection)
            {

                throw new Exception(expection.Message, expection);
            }
        }
    }
}
