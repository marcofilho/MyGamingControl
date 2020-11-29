using FluentValidation;

namespace MyGameIO.Business.Models.Validations
{
    public class JogoValidation : AbstractValidator<Jogo>
    {
        public JogoValidation()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}