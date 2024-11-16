using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PowerNow.API.Domain.Entities
{
    [Table("TB_PREVISAO_GERACAO_ENERGIA")]
    public class PrevisaoGeracaoEnergiaEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public double QuantidadeGerada { get; set; } // em kWh
        public double Temperatura { get; set; } // em °C
        public double RadiacaoSolar { get; set; } // em kW/m²
        public string Cep { get; set; } = string.Empty;
        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }
    }
}
