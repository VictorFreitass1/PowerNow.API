using PowerNow.API.Application.Services;
using PowerNow.API.Data.AppData;
using PowerNow.API.Data.Repositories;
using PowerNow.API.Domain.Interfaces;
using PowerNow.API.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PowerNow.API.IoC
{
    public class Bootstrap
    {
        public static void Start(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(x => {
                x.UseOracle(configuration["ConnectionStrings:Oracle"]);
            });


            services.AddTransient<IEnderecoService, EnderecoService>();
            services.AddTransient<IPrevisaoGeracaoEnergiaRepository, PrevisaoGeracaoEnergiaRepository>();
            services.AddTransient<IPrevisaoGeracaoEnergiaApplicationService, PrevisaoGeracaoEnergiaApplicationService>();
        }
    }
}
