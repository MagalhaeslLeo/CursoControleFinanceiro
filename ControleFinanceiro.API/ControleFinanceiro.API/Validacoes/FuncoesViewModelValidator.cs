using ControleFinanceiro.API.ViewModels;
using ControleFInanceiro.BLL.Models;
using FluentValidation;

namespace ControleFinanceiro.API.Validacoes
{
  public class FuncoesViewModelValidator : AbstractValidator<FuncoesViewModel>
  {
    public FuncoesViewModelValidator()
    {
      RuleFor(f => f.Name)
        .NotNull().WithMessage("Preencha a função")
        .NotEmpty().WithMessage("Preencha a função")
        .MinimumLength(6).WithMessage("Use mais caracteres")
        .MaximumLength(30).WithMessage("Use menos caracteres");

      RuleFor(f => f.Descricao)
        .NotNull().WithMessage("Preencha a descrição")
        .NotEmpty().WithMessage("Preencha a descrição")
        .MinimumLength(1).WithMessage("Use mais caracteres")
        .MaximumLength(50).WithMessage("Use menos caracteres");
    }
  }
}
