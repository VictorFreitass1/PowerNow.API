using PowerNow.API.Domain.Entities;
using PowerNow.API.Domain.Interfaces;
using PowerNow.API.Domain.Interfaces.Dtos;

namespace PowerNow.API.Application.Services
{
    public class PrevisaoGeracaoEnergiaApplicationService : IPrevisaoGeracaoEnergiaApplicationService
    {
    private readonly IPrevisaoGeracaoEnergiaRepository _previsaoGeracaoEnergiaRepository;
    private readonly IEnderecoService _enderecoService;

    public PrevisaoGeracaoEnergiaApplicationService(IPrevisaoGeracaoEnergiaRepository produtoRepository, IEnderecoService enderecoService)
    {
        _previsaoGeracaoEnergiaRepository = produtoRepository;
        _enderecoService = enderecoService;
    }

    public PrevisaoGeracaoEnergiaEntity? DeletarDadosPrevisoes(int id)
    {
        return _previsaoGeracaoEnergiaRepository.DeletarDados(id);
    }

    public PrevisaoGeracaoEnergiaEntity? EditarDadosPrevisoes(int id, IPrevisaoGeracaoEnergiaDto entity)
    {
        entity.Validate();

        return _previsaoGeracaoEnergiaRepository.EditarDados(new PrevisaoGeracaoEnergiaEntity
        {
            Id = id,
            Data = entity.Data,
            QuantidadeGerada = entity.QuantidadeGerada,
            Temperatura = entity.Temperatura,
            RadiacaoSolar = entity.RadiacaoSolar,
            Cep = entity.Cep,
            Endereco = entity.Endereco,

        });
    }

    public PrevisaoGeracaoEnergiaEntity? ObterPrevisoesPorId(int id)
    {
        return _previsaoGeracaoEnergiaRepository.ObterPorId(id);
    }

    public IEnumerable<PrevisaoGeracaoEnergiaEntity> ObterTodasPrevisoes()
    {
        return _previsaoGeracaoEnergiaRepository.ObterTodos();
    }

    public PrevisaoGeracaoEnergiaEntity? SalvarDadosPrevisoes(IPrevisaoGeracaoEnergiaDto entity)
    {
        entity.Validate();

        return _previsaoGeracaoEnergiaRepository.SalvarDados(new PrevisaoGeracaoEnergiaEntity
        {
            Data = entity.Data,
            QuantidadeGerada = entity.QuantidadeGerada,
            Temperatura = entity.Temperatura,
            RadiacaoSolar = entity.RadiacaoSolar,
            Cep = entity.Cep,
            Endereco = entity.Endereco,
        });
    }
    public async Task<Endereco?> ObterEnderecoPorCepAsync(string cep)
    {
        var endereco = await _enderecoService.ObterEnderecoPorCepAsync(cep);

        if (endereco is not null)
            return endereco;

        return null;

    }
}
}
