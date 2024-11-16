using PowerNow.API.Domain.Entities;

namespace PowerNow.API.Domain.Interfaces
{
    public interface IEnderecoService
    {
        Task<Endereco?> ObterEnderecoPorCepAsync(string cep);
    }
}
