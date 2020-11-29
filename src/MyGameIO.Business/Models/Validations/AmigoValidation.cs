using FluentValidation;

namespace MyGameIO.Business.Models.Validations
{
    public class AmigoValidation : AbstractValidator<Amigo>
    {
        public AmigoValidation()
        {
            RuleFor(f => f.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(f => f.Documento.Length).Equal(11)
                .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
 
        }
    }
}