using PowerNow.API.Domain.Entities;
using PowerNow.API.Domain.Interfaces.Dtos;

namespace PowerNow.API.Domain.Interfaces
{
    public interface IPrevisaoGeracaoEnergiaApplicationService
    {
        IEnumerable<PrevisaoGeracaoEnergiaEntity> ObterTodasPrevisoes();
        PrevisaoGeracaoEnergiaEntity? ObterPrevisoesPorId(int id);
        PrevisaoGeracaoEnergiaEntity? SalvarDadosPrevisoes(IPrevisaoGeracaoEnergiaDto entity);
        PrevisaoGeracaoEnergiaEntity? EditarDadosPrevisoes(int id, IPrevisaoGeracaoEnergiaDto entity);
        PrevisaoGeracaoEnergiaEntity? DeletarDadosPrevisoes(int id);
        Task<Endereco?> ObterEnderecoPorCepAsync(string cep);
    }
}
