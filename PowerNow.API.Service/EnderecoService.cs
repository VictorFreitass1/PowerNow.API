using RestSharp;
using PowerNow.API.Domain.Entities;
using PowerNow.API.Domain.Interfaces;

namespace PowerNow.API.Service
{
    public class EnderecoService : IEnderecoService
    {
        private readonly RestClient _restClient;

        public EnderecoService()
        {
            _restClient = new RestClient("https://viacep.com.br/ws/");
        }

        public async Task<Endereco?> ObterEnderecoPorCepAsync(string cep)
        {
            var request = new RestRequest($"{cep}/json/", Method.Get);
            var response = await _restClient.ExecuteAsync<Endereco>(request);

            if (!response.IsSuccessful || response.Data == null)
                return null;

            return response.Data;
        }
    }
}
