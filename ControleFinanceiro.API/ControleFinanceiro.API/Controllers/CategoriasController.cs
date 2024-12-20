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
using Microsoft.AspNetCore.Authorization;

namespace ControleFinanceiro.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]

  public class CategoriasController : ControllerBase
  {
    private readonly ICategoriaRepositorio _categoriaRepositorio;

    public CategoriasController(ICategoriaRepositorio categoriaRepositorio)
    {
      _categoriaRepositorio = categoriaRepositorio;
    }

    // GET: api/Categorias
    [Authorize(Roles = "Administrador")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
    {
      return await _categoriaRepositorio.PegarTodos().ToListAsync();
    }

    // GET: api/Categorias/5
    [Authorize(Roles = "Administrador")]
    [HttpGet("{id}")]
    public async Task<ActionResult<Categoria>> GetCategoria(int id)
    {
      var categoria = await _categoriaRepositorio.PegarPeloID(id);

      if (categoria == null)
      {
        return NotFound();
      }

      return categoria;
    }

    [Authorize(Roles = "Administrador")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategoria(int id, Categoria categoria)
    {
      if (id != categoria.CategoriaID)
      {
        return BadRequest();
      }

      if (ModelState.IsValid)
      {
        await _categoriaRepositorio.Atualizar(categoria);

        return Ok(new
        {
          mensagem = $"Categoria {categoria.Nome} atualizada com sucesso"
        });
      }
      return BadRequest(ModelState);
    }

    [Authorize(Roles = "Administrador")]
    [HttpPost]
    public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
    {

      if (ModelState.IsValid)
      {
        await _categoriaRepositorio.Inserir(categoria);
        return Ok(new
        {
          mensagem = $"Categoria {categoria.Nome} cadastrada com sucesso"
        });
      }
      return BadRequest(ModelState);
    }

    [Authorize(Roles = "Administrador")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategoria(int id)
    {
      var categoria = await _categoriaRepositorio.PegarPeloID(id);
      if (categoria == null)
      {
        return NotFound();
      }

      await _categoriaRepositorio.Excluir(id);
      return Ok(new
      {
        mensagem = $"Categoria {categoria.Nome} excluída com sucesso"
      });
    }

    [Authorize(Roles = "Administrador")]
    [HttpGet("FiltrarCategorias/{nomeCategoria}")]
    public async Task<ActionResult<IEnumerable<Categoria>>> FiltrarCategorias(string nomeCategoria)
    {
      return await _categoriaRepositorio.FiltrarCategorias(nomeCategoria).ToListAsync();
    }

    [Authorize]
    [HttpGet("FiltrarCategoriasDespesas")]

    public async Task<ActionResult<IEnumerable<Categoria>>> FiltrarCategoriasDespesas()
    {
      return await _categoriaRepositorio.PegarCategoriasPeloTipo("Despesa").ToListAsync();
    }
  }
}
