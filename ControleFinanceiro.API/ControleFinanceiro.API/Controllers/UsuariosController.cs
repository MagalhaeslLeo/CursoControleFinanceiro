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
using System.IO;
using ControleFinanceiro.API.ViewModels;
using Microsoft.AspNetCore.Identity;
using ControleFinanceiro.API.Services;

namespace ControleFinanceiro.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsuariosController : ControllerBase
  {
    private readonly IUsuarioRepositorio _usuarioRepositorio;

    public UsuariosController(IUsuarioRepositorio usuarioRepositorio)
    {
      _usuarioRepositorio = usuarioRepositorio;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Usuario>> GetUsuario(string id)
    {
      var usuario = await _usuarioRepositorio.PegarPeloID(id);

      if (usuario == null)
      {
        return NotFound();
      }

      return usuario;
    }
    [HttpPost("SalvarFoto")]
    public async Task<ActionResult> SalvarFoto()
    {
      var foto = Request.Form.Files[0];
      byte[] b;
      //OpenReadStream nos permite ler um arquivo que foi feito seu upload
      using (var openReadStream = foto.OpenReadStream())
      {
        //Copiar arquivo para memória
        using (var memoryStream = new MemoryStream())
        {
          //Transformo os bytes em array e copio para meu array de bytes
          await openReadStream.CopyToAsync(memoryStream);
          b = memoryStream.ToArray();
        }
      }
      return Ok(new
      {
        foto = b
      });
    }

    [HttpPost("RegistrarUsuario")]
    public async Task<ActionResult> RegistrarUsuario(RegistroViewModel model)
    {
      if (ModelState.IsValid)
      {
        IdentityResult usuarioCriado;
        string funcaoUsuario;

        Usuario usuario = new Usuario
        {
          UserName = model.NomeUsuario,
          Email = model.Email,
          PasswordHash = model.Senha,
          CPF = model.CPF,
          Profissao = model.Profissao,
          Foto = model.Foto
        };

        if (await _usuarioRepositorio.PegarQuantidadeUsuariosRegistrados() > 0)
        {
          funcaoUsuario = "Usuario";
        }
        else
        {
          funcaoUsuario = "Administrador";
        }
        usuarioCriado = await _usuarioRepositorio.CriarUsuario(usuario, model.Senha);
        if (usuarioCriado.Succeeded)
        {
          await _usuarioRepositorio.IncluirUsuarioEmFuncao(usuario, funcaoUsuario);
          var token = TokenService.GerarToken(usuario, funcaoUsuario);
          await _usuarioRepositorio.LogarUsuario(usuario, false);

          return Ok(new
          {
            emailUsuarioLogado = usuario.Email,
            usuarioID = usuario.Id,
            tokenUsuarioLogado = token
          });
        }
        else
        {
          return BadRequest(model);
        }
      }
      return BadRequest(model);
    }

    [HttpPost("LogarUsuario")]
    public async Task<ActionResult> LogarUsuario(LoginViewModel model)
    {
      if(model == null)
      {
        return NotFound("Usuários e / ou senhas inválidos");
      }
      Usuario usuario = await _usuarioRepositorio.PegarUsuarioPeloEmail(model.Email);
      if(usuario != null)
      {
        //Detectar a senha do usuário
        PasswordHasher<Usuario> passwordHasher = new PasswordHasher<Usuario>();
        if(passwordHasher.VerifyHashedPassword(usuario, usuario.PasswordHash, model.Senha) != PasswordVerificationResult.Failed)
        {
          var funcoesUsuario = await _usuarioRepositorio.PegarFuncoesUsuario(usuario);
          var token = TokenService.GerarToken(usuario, funcoesUsuario.First());
          await _usuarioRepositorio.LogarUsuario(usuario, false);
          return Ok(new
          {
            emailUsuarioLogado =usuario.Email,
            usuarioId = usuario.Id,
            tokenUsuarioLogado = token
          });
        }
        return NotFound("Usuário e / ou senha inválidos");
      }
      return NotFound("Usuário e / ou senha inválidos");
    }
  }
}
