using DesafioRPA_AeC.Domain.Entities;
using DesafioRPA_AeC.Infrastructure.Repositories;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace DesafioRPA_AeC.Api.Services
{
    public interface ICursosService
    {
        void BuscarCursoPorNome(string name);
    }

    public class CursosService:ICursosService
    {

        static private EdgeDriver _driver;

        private readonly ICursoRepository _cursoRepository;

        public CursosService(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        public void BuscarCursoPorNome(string nome)
        {
            Console.WriteLine("Buscando pelo curso: "+ nome);
            Thread.Sleep(1000);
            var options = new EdgeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal
            };

            var path = "..\\..\\..\\Driver";
            _driver = new EdgeDriver(path, options);
            _driver.Navigate().GoToUrl($"https://www.alura.com.br/busca?query={nome}");
            _driver.Manage().Window.Maximize();

            List<Curso> cursos = new();

            var resultados = _driver.FindElements(By.ClassName("busca-resultado"));

            foreach (var resultado in resultados)
            {
                Curso curso = new();
                var link = resultado.FindElement(By.ClassName("busca-resultado-link")).GetAttribute("href");
                curso.Titulo = resultado.FindElement(By.ClassName("busca-resultado-nome")).Text;
                curso.Descricao = resultado.FindElement(By.ClassName("busca-resultado-descricao")).Text;
                curso.LinkAcesso = link;

                _driver.SwitchTo().NewWindow(WindowType.Tab);
                _driver.Navigate().GoToUrl(link);





                try
                {
                    curso.Professor = _driver.FindElement(By.ClassName("instructor-title--name")).Text;
                    curso.CargaHoraria = _driver.FindElement(By.ClassName("courseInfo-card-wrapper-infos")).Text;
                }
                catch
                {
                    try
                    {
                        curso.Professor = _driver.FindElement(By.ClassName("formacao-instrutor-nome")).GetAttribute("innerText");
                        curso.CargaHoraria = _driver.FindElement(By.ClassName("formacao__info-destaque")).Text;
                    }
                    catch
                    {
                        try
                        {
                            curso.Professor = _driver.FindElements(By.TagName("li"))[9].Text;
                            curso.CargaHoraria = _driver.FindElement(By.ClassName("episode-programming-time")).Text;
                        }
                        catch
                        {
                            curso.Professor = "Não disponível";
                            curso.CargaHoraria = "Não disponível";
                        }
                    }
                }




                _driver.Close();
                _driver.SwitchTo().Window(_driver.WindowHandles.First());
                cursos.Add(curso);

            }
            _driver.Close();
            _cursoRepository.SalvaCursos(cursos);
        }


    }
}
