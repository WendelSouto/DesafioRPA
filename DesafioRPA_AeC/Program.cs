
using DesafioRPA_AeC.Api.Services;
using DesafioRPA_AeC.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

class Program
{

    static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var cursoService = serviceProvider.GetService<ICursosService>();

        cursoService.BuscarCursoPorNome(args[0]);
        
    }

    public static void ConfigureServices(IServiceCollection service)
    {
        service.AddScoped<ICursosService, CursosService>().
            AddScoped<ICursoRepository, CursoRepository>();
    }

}