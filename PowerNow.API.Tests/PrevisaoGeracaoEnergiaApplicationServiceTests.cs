using Moq;
using PowerNow.API.Application.Services;
using PowerNow.API.Domain.Entities;
using PowerNow.API.Domain.Interfaces;
using PowerNow.API.Domain.Interfaces.Dtos;

namespace PowerNow.API.Tests
{
    public class PrevisaoGeracaoEnergiaApplicationServiceTests
    {
        private readonly Mock<IEnderecoService> _enderecoServiceMock;
        private readonly Mock<IPrevisaoGeracaoEnergiaRepository> _repositoryMock;
        private readonly PrevisaoGeracaoEnergiaApplicationService _previsaoGeracaoEnergiaService;

        public PrevisaoGeracaoEnergiaApplicationServiceTests()
        {
            _repositoryMock = new Mock<IPrevisaoGeracaoEnergiaRepository>();
            _enderecoServiceMock = new Mock<IEnderecoService>();

            _previsaoGeracaoEnergiaService = new PrevisaoGeracaoEnergiaApplicationService(_repositoryMock.Object, _enderecoServiceMock.Object);
        }

        [Fact]
        public void SalvarDadosPrevisaoGeracaoEnergia_DeveRetornarPrevisaoGeracaoEnergiaEntity_QuandoAdicionarComSucesso()
        {
            // Arrange
            var previsaoGeracaoEnergiaDtoMock = new Mock<IPrevisaoGeracaoEnergiaDto>();
            previsaoGeracaoEnergiaDtoMock.Setup(c => c.Cep).Returns("12345-678");
            previsaoGeracaoEnergiaDtoMock.Setup(c => c.Data).Returns(DateTime.Now);
            previsaoGeracaoEnergiaDtoMock.Setup(c => c.QuantidadeGerada).Returns(150.5);
            previsaoGeracaoEnergiaDtoMock.Setup(c => c.Temperatura).Returns(25.0);
            previsaoGeracaoEnergiaDtoMock.Setup(c => c.RadiacaoSolar).Returns(1.2);

            var previsaoGeracaoEnergiaEsperado = new PrevisaoGeracaoEnergiaEntity
            {
                Id = 1,
                Data = DateTime.Now,
                QuantidadeGerada = 150.5,
                Temperatura = 25.0,
                RadiacaoSolar = 1.2,
                Cep = "12345-678",
                Endereco = new Endereco
                {
                    Cep = "12345-678",
                    Logradouro = "Rua Teste",
                    Bairro = "Bairro Teste",
                    Uf = "SP"
                }
            };

            _repositoryMock
                .Setup(r => r.SalvarDados(It.IsAny<PrevisaoGeracaoEnergiaEntity>()))
                .Returns(previsaoGeracaoEnergiaEsperado);

            // Act
            var resultado = _previsaoGeracaoEnergiaService.SalvarDadosPrevisoes(previsaoGeracaoEnergiaDtoMock.Object);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(previsaoGeracaoEnergiaEsperado.Id, resultado.Id);
            Assert.Equal(previsaoGeracaoEnergiaEsperado.Cep, resultado.Cep);
        }

        [Fact]
        public void EditarDadosPrevisaoGeracaoEnergia_DeveRetornarPrevisaoGeracaoEnergiaEntity_QuandoEditarComSucesso()
        {
            // Arrange
            var previsaoGeracaoEnergiaDtoMock = new Mock<IPrevisaoGeracaoEnergiaDto>();
            previsaoGeracaoEnergiaDtoMock.Setup(c => c.Cep).Returns("12345-678");
            previsaoGeracaoEnergiaDtoMock.Setup(c => c.Data).Returns(DateTime.Now);
            previsaoGeracaoEnergiaDtoMock.Setup(c => c.QuantidadeGerada).Returns(200.0);
            previsaoGeracaoEnergiaDtoMock.Setup(c => c.Temperatura).Returns(30.0);
            previsaoGeracaoEnergiaDtoMock.Setup(c => c.RadiacaoSolar).Returns(1.8);

            var previsaoGeracaoEnergiaEsperado = new PrevisaoGeracaoEnergiaEntity
            {
                Id = 1,
                Data = DateTime.Now,
                QuantidadeGerada = 200.0,
                Temperatura = 30.0,
                RadiacaoSolar = 1.8,
                Cep = "12345-678",
                Endereco = new Endereco
                {
                    Cep = "12345-678",
                    Logradouro = "Rua Atualizada",
                    Bairro = "Bairro Atualizado",
                    Uf = "SP"
                }
            };

            _repositoryMock
                .Setup(r => r.EditarDados(It.IsAny<PrevisaoGeracaoEnergiaEntity>()))
                .Returns(previsaoGeracaoEnergiaEsperado);

            // Act
            var resultado = _previsaoGeracaoEnergiaService.EditarDadosPrevisoes(1, previsaoGeracaoEnergiaDtoMock.Object);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(previsaoGeracaoEnergiaEsperado.Id, resultado.Id);
            Assert.Equal(previsaoGeracaoEnergiaEsperado.Cep, resultado.Cep);
            Assert.Equal(previsaoGeracaoEnergiaEsperado.Endereco.Logradouro, resultado.Endereco.Logradouro);
        }


        [Fact]
        public void ObterPrevisaoGeracaoEnergiaPorId_DeveRetornarPrevisaoGeracaoEnergiaEntity_QuandoPrevisaoGeracaoEnergiaExiste()
        {
            // Arrange
            var previsaoGeracaoEnergiaEsperado = new PrevisaoGeracaoEnergiaEntity
            {
                Id = 1,
                Data = DateTime.Now,
                QuantidadeGerada = 120.0,
                Temperatura = 28.0,
                RadiacaoSolar = 1.5,
                Cep = "12345-678",
                Endereco = new Endereco
                {
                    Cep = "12345-678",
                    Logradouro = "Rua Teste",
                    Bairro = "Bairro Teste",
                    Uf = "SP"
                }
            };

            _repositoryMock.Setup(r => r.ObterPorId(1)).Returns(previsaoGeracaoEnergiaEsperado);

            // Act
            var resultado = _previsaoGeracaoEnergiaService.ObterPrevisoesPorId(1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(previsaoGeracaoEnergiaEsperado.Id, resultado.Id);
            Assert.Equal(previsaoGeracaoEnergiaEsperado.Cep, resultado.Cep);
            Assert.Equal(previsaoGeracaoEnergiaEsperado.Endereco.Logradouro, resultado.Endereco.Logradouro);
        }

        [Fact]
        public void ObterTodosPrevisaoGeracaoEnergia_DeveRetornarListaDePrevisaoGeracaoEnergia_QuandoExistiremPrevisaoGeracaoEnergia()
        {
            // Arrange
            var previsaoGeracaoEnergiaEsperados = new List<PrevisaoGeracaoEnergiaEntity>
            {
                new PrevisaoGeracaoEnergiaEntity
                {
                    Id = 1,
                    Cep = "12345-678",
                    Data = DateTime.Now,
                    QuantidadeGerada = 120.0,
                    Endereco = new Endereco
                    {
                        Cep = "12345-678",
                        Logradouro = "Rua Teste",
                        Bairro = "Bairro Teste",
                        Uf = "SP"
                    }
                },
                new PrevisaoGeracaoEnergiaEntity
                {
                    Id = 2,
                    Cep = "98765-432",
                    Data = DateTime.Now,
                    QuantidadeGerada = 130.0,
                    Endereco = new Endereco
                    {
                        Cep = "98765-432",
                        Logradouro = "Rua Exemplo",
                        Bairro = "Bairro Exemplo",
                        Uf = "RJ"
                    }
                }
            };

            _repositoryMock.Setup(r => r.ObterTodos()).Returns(previsaoGeracaoEnergiaEsperados);

            // Act
            var resultado = _previsaoGeracaoEnergiaService.ObterTodasPrevisoes();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            Assert.Equal(previsaoGeracaoEnergiaEsperados.First().Cep, resultado.First().Cep);
        }

        [Fact]
        public void RemoverPrevisaoGeracaoEnergia_DeveRetornarPrevisaoGeracaoEnergiaEntity_QuandoRemoverComSucesso()
        {
            // Arrange
            var previsaoGeracaoEnergiaEsperado = new PrevisaoGeracaoEnergiaEntity
            {
                Id = 1,
                Data = DateTime.Now,
                QuantidadeGerada = 120.0,
                Temperatura = 25.0,
                RadiacaoSolar = 1.2,
                Cep = "12345-678",
                Endereco = new Endereco
                {
                    Cep = "12345-678",
                    Logradouro = "Rua Removida",
                    Bairro = "Bairro Removido",
                    Uf = "SP"
                }
            };

            _repositoryMock
                .Setup(r => r.DeletarDados(1))
                .Returns(previsaoGeracaoEnergiaEsperado);

            // Act
            var resultado = _previsaoGeracaoEnergiaService.DeletarDadosPrevisoes(1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(previsaoGeracaoEnergiaEsperado.Id, resultado.Id);
            Assert.Equal(previsaoGeracaoEnergiaEsperado.Cep, resultado.Cep);
            Assert.Equal(previsaoGeracaoEnergiaEsperado.Endereco.Logradouro, resultado.Endereco.Logradouro);
        }

    }
}
