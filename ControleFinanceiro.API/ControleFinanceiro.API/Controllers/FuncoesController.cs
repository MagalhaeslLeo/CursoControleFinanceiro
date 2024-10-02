using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControleFInanceiro.BLL.Models;
using ControleFinanceiro.DAL;
using ControleFinanceiro.DAL.Interfaces;
using ControleFinanceiro.API.ViewModels;

namespace ControleFinanceiro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncoesController : ControllerBase
    {
        private readonly IFuncaoRepositorio _funcaoRepositorio;

        public FuncoesController(IFuncaoRepositorio funcaoRepositorio)
        {
            _funcaoRepositorio = funcaoRepositorio;
        }

        // GET: api/Funcoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcao>>> GetFuncoes()
        {
            return await _funcaoRepositorio.PegarTodos().ToListAsync();
        }

        // GET: api/Funcoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Funcao>> GetFuncao(string id)
        {
            var funcao = await _funcaoRepositorio.PegarPeloID(id);

            if (funcao == null)
            {
                return NotFound();
            }

            return funcao;
        }

        // PUT: api/Funcoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFuncao(string id, FuncoesViewModel funcoes)
        {
            if (id != funcoes.ID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                Funcao funcao = new Funcao
                {
                    Id = funcoes.ID,
                    Name = funcoes.Name,
                    Descricao = funcoes.Descricao
                };
                await _funcaoRepositorio.AtualizarFuncao(funcao);
                return Ok(new
                {
                    mensagem = $"Função {funcao.Name} atualizada com sucesso"
                });
            }
            return BadRequest(ModelState);
        }


        [HttpPost]
        public async Task<ActionResult<Funcao>> PostFuncao(FuncoesViewModel funcoes)
        {
            if (ModelState.IsValid)
            {
                Funcao funcao = new Funcao
                {
                    Name = funcoes.Name,
                    Descricao = funcoes.Descricao
                };
                await _funcaoRepositorio.AdicionarFuncao(funcao);
                return Ok(new
                {
                    mensagem = $"Função {funcao.Name} adicionada com sucesso"
                });
            }

            return BadRequest(ModelState);
        }

        // DELETE: api/Funcoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFuncao(string id)
        {
            var funcao = await _funcaoRepositorio.PegarPeloID(id);
            if (funcao == null)
            {
                return NotFound();
            }

            await _funcaoRepositorio.Excluir(funcao);
            return Ok(new
            {
                mensagem = $"Função {funcao.Name} excluída com sucesso"
            });
        }

        [HttpGet("FiltrarFuncoes/{nomeFuncao}")]

        public async Task<ActionResult<IEnumerable<Funcao>>> FiltrarFuncoes(string nomeFuncao)
        {
            return await _funcaoRepositorio.FiltrarFuncoes(nomeFuncao).ToListAsync();
        }
    }
}
