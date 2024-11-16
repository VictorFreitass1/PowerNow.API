using PowerNow.API.Domain.Entities;

namespace PowerNow.API.Domain.Interfaces.Dtos
{
    public interface IPrevisaoGeracaoEnergiaDto
    {
        public DateTime Data { get; set; }
        public double QuantidadeGerada { get; set; }
        public double Temperatura { get; set; }
        public double RadiacaoSolar { get; set; }
        public string Cep { get; set; }
        public Endereco Endereco { get; set; }
        void Validate();
    }
}
