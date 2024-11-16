using Microsoft.EntityFrameworkCore;
using PowerNow.API.Data.AppData;
using PowerNow.API.Domain.Entities;
using PowerNow.API.Data.Repositories;

namespace PowerNow.API.Tests
{
    public class PrevisaoGeracaoEnergiaRepositoryTests
    {
        private readonly DbContextOptions<ApplicationContext> _options;
        private readonly ApplicationContext _context;
        private readonly PrevisaoGeracaoEnergiaRepository _previsaoGeracaoEnergiaRepository;

        public PrevisaoGeracaoEnergiaRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationContext(_options);
            _previsaoGeracaoEnergiaRepository = new PrevisaoGeracaoEnergiaRepository(_context);
        }

        [Fact]
        public void SalvarDados_DeveAdicionarPrevisaoGeracaoEnergiaERetornarPrevisaoGeracaoEnergiaEntity()
        {
            // Arrange
            var previsao = new PrevisaoGeracaoEnergiaEntity
            {
                Data = DateTime.Now,
                QuantidadeGerada = 150.0,
                Temperatura = 28.0,
                RadiacaoSolar = 1.5,
                Cep = "12345-678"
            };

            // Act
            var resultado = _previsaoGeracaoEnergiaRepository.SalvarDados(previsao);

            // Assert
            var previsaoNoDb = _context.PrevisaoGeracaoEnergia.FirstOrDefault(c => c.Id == resultado.Id);
            Assert.NotNull(previsaoNoDb);
            Assert.Equal(previsao.QuantidadeGerada, previsaoNoDb.QuantidadeGerada);
            Assert.Equal(previsao.Cep, previsaoNoDb.Cep);
        }

        [Fact]
        public void EditarDados_DeveAtualizarPrevisaoGeracaoEnergiaERetornarPrevisaoGeracaoEnergiaEntity_QuandoPrevisaoExiste()
        {
            // Arrange
            var previsao = new PrevisaoGeracaoEnergiaEntity
            {
                Data = DateTime.Now,
                QuantidadeGerada = 150.0,
                Temperatura = 28.0,
                RadiacaoSolar = 1.5,
                Cep = "12345-678"
            };
            _context.PrevisaoGeracaoEnergia.Add(previsao);
            _context.SaveChanges();

            previsao.QuantidadeGerada = 200.0;
            previsao.Temperatura = 30.0;

            // Act
            var resultado = _previsaoGeracaoEnergiaRepository.EditarDados(previsao);

            // Assert
            var previsaoNoDb = _context.PrevisaoGeracaoEnergia.FirstOrDefault(c => c.Id == previsao.Id);
            Assert.NotNull(previsaoNoDb);
            Assert.Equal(200.0, previsaoNoDb.QuantidadeGerada);
            Assert.Equal(30.0, previsaoNoDb.Temperatura);
        }

        [Fact]
        public void ObterPorId_DeveRetornarPrevisaoGeracaoEnergiaEntity_QuandoPrevisaoExiste()
        {
            // Arrange
            var previsao = new PrevisaoGeracaoEnergiaEntity
            {
                Data = DateTime.Now,
                QuantidadeGerada = 120.0,
                Temperatura = 25.0,
                RadiacaoSolar = 1.2,
                Cep = "12345-678"
            };
            _context.PrevisaoGeracaoEnergia.Add(previsao);
            _context.SaveChanges();

            // Act
            var resultado = _previsaoGeracaoEnergiaRepository.ObterPorId(previsao.Id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(previsao.Id, resultado.Id);
            Assert.Equal(previsao.QuantidadeGerada, resultado.QuantidadeGerada);
        }

        [Fact]
        public void ObterTodos_DeveRetornarListaDePrevisoesGeracaoEnergia_QuandoExistiremPrevisoes()
        {
            // Arrange
            _context.PrevisaoGeracaoEnergia.RemoveRange(_context.PrevisaoGeracaoEnergia);
            _context.SaveChanges();

            var previsoes = new List<PrevisaoGeracaoEnergiaEntity>
            {
                new PrevisaoGeracaoEnergiaEntity
                {
                    Data = DateTime.Now,
                    QuantidadeGerada = 150.0,
                    Temperatura = 28.0,
                    RadiacaoSolar = 1.5,
                    Cep = "12345-678"
                },
                new PrevisaoGeracaoEnergiaEntity
                {
                    Data = DateTime.Now,
                    QuantidadeGerada = 200.0,
                    Temperatura = 30.0,
                    RadiacaoSolar = 1.8,
                    Cep = "98765-432"
                }
            };

            _context.PrevisaoGeracaoEnergia.AddRange(previsoes);
            _context.SaveChanges();

            // Act
            var resultado = _previsaoGeracaoEnergiaRepository.ObterTodos();

            // Assert
            Assert.Equal(previsoes.Count, resultado.Count());
            Assert.Contains(resultado, p => p.Cep == "12345-678");
            Assert.Contains(resultado, p => p.Cep == "98765-432");
        }

        [Fact]
        public void DeletarDados_DeveRemoverPrevisaoGeracaoEnergiaERetornarPrevisaoGeracaoEnergiaEntity_QuandoPrevisaoExiste()
        {
            // Arrange
            var previsao = new PrevisaoGeracaoEnergiaEntity
            {
                Data = DateTime.Now,
                QuantidadeGerada = 150.0,
                Temperatura = 28.0,
                RadiacaoSolar = 1.5,
                Cep = "12345-678"
            };
            _context.PrevisaoGeracaoEnergia.Add(previsao);
            _context.SaveChanges();

            // Act
            var resultado = _previsaoGeracaoEnergiaRepository.DeletarDados(previsao.Id);

            // Assert
            var previsaoNoDb = _context.PrevisaoGeracaoEnergia.FirstOrDefault(c => c.Id == previsao.Id);

            Assert.Null(previsaoNoDb);
            Assert.Equal(previsao, resultado);
        }
    }
}
