using PowerNow.API.Domain.Entities;

namespace PowerNow.API.Domain.Interfaces
{
    public interface IPrevisaoGeracaoEnergiaRepository
    {
        PrevisaoGeracaoEnergiaEntity? ObterPorId(int id);
        IEnumerable<PrevisaoGeracaoEnergiaEntity>? ObterTodos();
        PrevisaoGeracaoEnergiaEntity? SalvarDados(PrevisaoGeracaoEnergiaEntity previsao);
        PrevisaoGeracaoEnergiaEntity? EditarDados(PrevisaoGeracaoEnergiaEntity previsao);
        PrevisaoGeracaoEnergiaEntity? DeletarDados(int id);
    }
}
