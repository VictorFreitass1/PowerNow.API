using PowerNow.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace PowerNow.API.Data.AppData
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
        public DbSet<PrevisaoGeracaoEnergiaEntity> PrevisaoGeracaoEnergia { get; set; }
    }
}
