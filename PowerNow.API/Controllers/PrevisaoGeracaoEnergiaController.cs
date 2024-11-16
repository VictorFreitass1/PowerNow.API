using PowerNow.API.Application.Dtos;
using PowerNow.API.Domain.Entities;
using PowerNow.API.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PowerNow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrevisaoGeracaoEnergiaController : ControllerBase
    {
        private readonly IPrevisaoGeracaoEnergiaApplicationService _previsaoGeracaoEnergiaApplicationService;

        public PrevisaoGeracaoEnergiaController(IPrevisaoGeracaoEnergiaApplicationService previsaoGeracaoEnergiaApplicationService)
        {
            _previsaoGeracaoEnergiaApplicationService = previsaoGeracaoEnergiaApplicationService;
        }

        /// <summary>
        /// Metodo para obter todos os dados de previsão
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces<IEnumerable<PrevisaoGeracaoEnergiaEntity>>]
        public IActionResult Get()
        {
            var previsao = _previsaoGeracaoEnergiaApplicationService.ObterTodasPrevisoes();

            if (previsao is not null)
                return Ok(previsao);

            return BadRequest("Não foi possível obter os dados");
        }

        /// <summary>
        /// Método para obter uma previsao
        /// </summary>
        /// <param name="id">Identificador da previsao</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces<PrevisaoGeracaoEnergiaEntity>]

        public IActionResult GetPorId(int id)
        {
            var previsao = _previsaoGeracaoEnergiaApplicationService.ObterPrevisoesPorId(id);

            if (previsao is not null)
                return Ok(previsao);

            return BadRequest("Não foi possível obter os dados");
        }


        /// <summary>
        /// Método para salvar a previsao
        /// </summary>
        /// <param name="entity">Modelo de dados da Previsao</param>
        /// <returns></returns>
        [HttpPost]
        [Produces<PrevisaoGeracaoEnergiaEntity>]
        public IActionResult Post(PrevisaoGeracaoEnergiaDto entity)
        {
            try
            {
                var previsao = _previsaoGeracaoEnergiaApplicationService.SalvarDadosPrevisoes(entity);

                if (previsao is not null)
                    return Ok(previsao);

                return BadRequest("Não foi possivel salvar os dados");
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Error = ex.Message,
                    status = HttpStatusCode.BadRequest,
                });
            }
        }

        /// <summary>
        /// Método para editar o previsao
        /// </summary>
        /// <param name="id"> Identificador da Previsao</param>
        /// <param name="entity">Modelo de dados do Previsao</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces<PrevisaoGeracaoEnergiaEntity>]
        public IActionResult Put(int id, [FromBody] PrevisaoGeracaoEnergiaDto entity)
        {
            try
            {
                var previsao = _previsaoGeracaoEnergiaApplicationService.EditarDadosPrevisoes(id, entity);

                if (previsao is not null)
                    return Ok(previsao);

                return BadRequest("Não foi possivel salvar os dados");
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Error = ex.Message,
                    status = HttpStatusCode.BadRequest,
                });
            }
        }


        /// <summary>
        /// Método para deletar um previsao
        /// </summary>
        /// <param name="id">Identificador da Previsao</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces<PrevisaoGeracaoEnergiaEntity>]

        public IActionResult Delete(int id)
        {
            var previsao = _previsaoGeracaoEnergiaApplicationService.DeletarDadosPrevisoes (id);

            if (previsao is not null)
                return Ok(previsao);

            return BadRequest("Não foi possível deletar os dados");
        }

        [HttpGet("busca/endereco/{cep}")]
        [Produces<Endereco>]
        public async Task<IActionResult> GetDataService(string cep)
        {
            var endereco = await _previsaoGeracaoEnergiaApplicationService.ObterEnderecoPorCepAsync(cep);

            if (endereco is not null)
                return Ok(endereco);

            return BadRequest("Não foi possível obter os dados do endereço");
        }
    }
}
