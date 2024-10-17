using System;

namespace ControleFinanceiro.API
{
  public static class Settings
  {
    //Com base nessa chave o token do usuário será gerado
    public static string ChaveSecreta = Guid.NewGuid().ToString();
  }
}
