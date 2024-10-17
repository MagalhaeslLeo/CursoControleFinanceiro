using ControleFInanceiro.BLL.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ControleFinanceiro.API.Services
{
  public static class TokenService
  {
    public static string GerarToken(Usuario usuario, string funcaoUsuario)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var chave = Encoding.ASCII.GetBytes(Settings.ChaveSecreta);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        //Gerar conjunto de informações para identificar o usuário
        Subject = new ClaimsIdentity(new Claim[]
        {
          new Claim(ClaimTypes.Name, usuario.UserName.ToString()),
          new Claim(ClaimTypes.Role, funcaoUsuario)
        }),
        //Configurar quando o token vai expirar
        Expires = DateTime.UtcNow.AddHours(2),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha256Signature)
      };
      //Criando o token passando as informações
      var token = tokenHandler.CreateToken(tokenDescriptor);
      //Serializar o token em formato JWT e retorná-lo
      return tokenHandler.WriteToken(token);
    }
  }
}
