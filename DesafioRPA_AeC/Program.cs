using DesafioRPA_AeC.Api.Services;
using DesafioRPA_AeC.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    public static void ConfigureServices(IServiceCollection service)
    {
        service.AddScoped<ICursosService, CursosService>().
            AddScoped<ICursoRepository, CursoRepository>();
    }
    static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var cursoService = serviceProvider.GetService<ICursosService>();

        try
        {
            if (args[0] == "--help") { GetMessageHelp(); return; }
            if (args.Length == 2) cursoService.BuscarCursoPorNome(args[0], args[1]);
            if (args.Length == 1) cursoService.BuscarCursoPorNome(args[0]);
        } catch
        {
            GetMessageHelp();
        }
    }

    private static void GetMessageHelp()
    {
        Console.WriteLine("Como usar: ");
        Console.WriteLine("DesafioRPA_AeC.exe [nome do curso]\tExecuta a busca de um curso e salva na pasta raiz do projeto");
        Console.WriteLine("DesafioRPA_AeC.exe [nome do curso] [diretorio de saida]\tExecuta a busca de um curso e salva no diretorio escolhido");
        Console.WriteLine("Exemplo: ");
        Console.WriteLine("DesafioRPA_AeC.exe RPA");
        Console.WriteLine("DesafioRPA_AeC.exe RPA \"C:/temp/\"");
    }



}