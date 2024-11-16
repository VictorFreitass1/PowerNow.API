using PowerNow.API.Data.AppData;
using PowerNow.API.Domain.Entities;
using PowerNow.API.Domain.Interfaces;

namespace PowerNow.API.Data.Repositories
{
    public class PrevisaoGeracaoEnergiaRepository : IPrevisaoGeracaoEnergiaRepository
    {
        private readonly ApplicationContext _context;

        public PrevisaoGeracaoEnergiaRepository(ApplicationContext context)
        {
            _context = context;
        }

        public PrevisaoGeracaoEnergiaEntity? DeletarDados(int id)
        {
            var previsao = _context.PrevisaoGeracaoEnergia.Find(id);

            if (previsao is not null)
            {
                _context.PrevisaoGeracaoEnergia.Remove(previsao);
                _context.SaveChanges();

                return previsao;
            }
            return null;
        }

        public PrevisaoGeracaoEnergiaEntity? EditarDados(PrevisaoGeracaoEnergiaEntity entity)
        {
            var previsao = _context.PrevisaoGeracaoEnergia.Find(entity.Id);

            if (previsao is not null)
            {
                previsao.Data = entity.Data;
                previsao.QuantidadeGerada = entity.QuantidadeGerada;
                previsao.Temperatura = entity.Temperatura;
                previsao.RadiacaoSolar = entity.RadiacaoSolar;
                previsao.Cep = entity.Cep;

                _context.PrevisaoGeracaoEnergia.Update(previsao);
                _context.SaveChanges();

                return previsao;
            }
            return null;
        }

        public PrevisaoGeracaoEnergiaEntity? ObterPorId(int id)
        {
            var previsao = _context.PrevisaoGeracaoEnergia.Find(id);

            if (previsao is not null)
            {

                return previsao;
            }

            return null;
        }

        public IEnumerable<PrevisaoGeracaoEnergiaEntity> ObterTodos()
        {
            var entity = _context.PrevisaoGeracaoEnergia.ToList();

            return entity;
        }


        public PrevisaoGeracaoEnergiaEntity? SalvarDados(PrevisaoGeracaoEnergiaEntity entity)
        {
            _context.PrevisaoGeracaoEnergia.Add(entity);
            _context.SaveChanges();

            return entity;
        }
    }
}
