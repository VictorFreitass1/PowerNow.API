using FluentValidation;
using PowerNow.API.Domain.Entities;
using PowerNow.API.Domain.Interfaces.Dtos;

namespace PowerNow.API.Application.Dtos
{
    public class PrevisaoGeracaoEnergiaDto : IPrevisaoGeracaoEnergiaDto
    {
        public DateTime Data { get; set; }
        public double QuantidadeGerada { get; set; } // em kWh
        public double Temperatura { get; set; } // em °C
        public double RadiacaoSolar { get; set; } // em kW/m²
        public string Cep { get; set; } = string.Empty;
        public Endereco Endereco { get; set; }

        public void Validate()
        {
            var validateResult = new PrevisaoGeracaoEnergiaDtoValidation().Validate(this);

            if (!validateResult.IsValid)
                throw new Exception(string.Join(" e ", validateResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
    internal class PrevisaoGeracaoEnergiaDtoValidation : AbstractValidator<PrevisaoGeracaoEnergiaDto>
    {
        public PrevisaoGeracaoEnergiaDtoValidation()
        {
            RuleFor(x => x.Cep)
                .MinimumLength(5).WithMessage(x => $"O campo {nameof(x.Cep)} deve ter no mínimo 5 caracteres")
                .NotEmpty().WithMessage(x => $"O campo {nameof(x.Cep)} não pode ser vazio")
                .Matches(@"^\d{5}-\d{3}$").WithMessage(x => $"O campo {nameof(x.Cep)} deve seguir o formato XXXXX-XXX");

            RuleFor(x => x.Data)
                .GreaterThan(DateTime.Now).WithMessage(x => $"A {nameof(x.Data)} deve ser uma data futura");

            RuleFor(x => x.QuantidadeGerada)
                .GreaterThan(0).WithMessage(x => $"A {nameof(x.QuantidadeGerada)} deve ser maior que zero");

            RuleFor(x => x.Temperatura)
                .InclusiveBetween(-50, 60).WithMessage(x => $"A {nameof(x.Temperatura)} deve estar entre -50°C e 60°C");

            RuleFor(x => x.RadiacaoSolar)
                .InclusiveBetween(0, 10).WithMessage(x => $"A {nameof(x.RadiacaoSolar)} deve estar entre 0 e 10 kW/m²");
        }
    }
}
