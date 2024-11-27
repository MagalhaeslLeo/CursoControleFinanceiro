using ControleFinanceiro.DAL.Interfaces;
using ControleFInanceiro.BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleFinanceiro.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MesesController : ControllerBase
  {
    private readonly IMesRepositorio _mesRepositorio;

    public MesesController(IMesRepositorio mesRepositorio)
    {
      _mesRepositorio = mesRepositorio;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Mes>>> GetMeses()
    {
      return await _mesRepositorio.PegarTodos().ToListAsync();
    }
  }
}
