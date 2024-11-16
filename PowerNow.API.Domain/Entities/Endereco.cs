using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PowerNow.API.Domain.Entities
{
    [Table("TB_ENDERECO")]
    public class Endereco
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Uf { get; set; }
    }
}
