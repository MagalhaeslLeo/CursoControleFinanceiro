using ControleFinanceiro.DAL.Interfaces;
using ControleFInanceiro.BLL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL.Repositorios
{
    public class FuncaoRepositorio : RepositorioGenerico<Funcao>, IFuncaoRepositorio
    {
        private readonly Contexto _contexto;
        private readonly RoleManager<Funcao> _gerenciadorfuncoes;
        public FuncaoRepositorio(Contexto contexto,
            RoleManager<Funcao> gerenciadorfuncoes) : base(contexto)
        {
            _contexto = contexto;
            _gerenciadorfuncoes= gerenciadorfuncoes;
        }

        public async Task AdicionarFuncao(Funcao funcao)
        {
            try
            {
                await _gerenciadorfuncoes.CreateAsync(funcao);
            }
            catch (Exception expection)
            {

                throw new Exception(expection.Message, expection);
            }
        }

        public async Task AtualizarFuncao(Funcao funcao)
        {
            try
            {
                //Pega pelo ID e atualiza as propriedades com novos valores do objeto funcao
                Funcao f = await PegarPeloID(funcao.Id);
                f.Name = funcao.Name;
                f.NormalizedName = funcao.NormalizedName;
                f.Descricao = funcao.Descricao;

                await _gerenciadorfuncoes.UpdateAsync(f);

            }
            catch (Exception expection)
            {

                throw new Exception(expection.Message, expection);
            }
        }

        public IQueryable<Funcao> FiltrarFuncoes(string nomeFuncao)
        {
            try
            {
                var entity = _contexto.Funcoes.Where(f=>f.Name.Contains(nomeFuncao));
                return entity;
            }
            catch (Exception expection)
            {

                throw new Exception(expection.Message, expection);
            }
        }
    }
}
