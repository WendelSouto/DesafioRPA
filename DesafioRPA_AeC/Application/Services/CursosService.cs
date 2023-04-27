using DesafioRPA_AeC.Domain.Entities;
using DesafioRPA_AeC.Infrastructure.Repositories;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System.Configuration;

namespace DesafioRPA_AeC.Api.Services
{
    public interface ICursosService
    {
        void BuscarCursoPorNome(string name);
        string CheckTeacherElements(List<string> teacherElements);
        string CheckTimingElements(List<string> timingElements);
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
            var appSettings = ConfigurationManager.AppSettings;
            var path = "..\\..\\..\\Driver";

            List<string> teacherElements = appSettings["TeacherElements"].Split(",").ToList();
            List<string> timingElements = appSettings["TimingElements"].Split(",").ToList();

            var options = new EdgeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal
            };

            _driver = new EdgeDriver(path, options);
            _driver.Navigate().GoToUrl($"https://www.alura.com.br/busca?query={nome}");
            _driver.Manage().Window.Maximize();

            List<Curso> cursos = new();

            var resultados = _driver.FindElements(By.ClassName("busca-resultado"));

            foreach (var resultado in resultados)
            {
                Curso curso = new();
                var link = resultado.FindElement(By.ClassName("busca-resultado-link")).GetAttribute("href");
                curso.Titulo = resultado.FindElement(By.ClassName("busca-resultado-nome")).GetAttribute("innerText");
                curso.Descricao = resultado.FindElement(By.ClassName("busca-resultado-descricao")).GetAttribute("innerText");
                curso.LinkAcesso = link;

                _driver.SwitchTo().NewWindow(WindowType.Tab);
                _driver.Navigate().GoToUrl(link);

                curso.Professor = CheckTeacherElements(teacherElements);
                curso.CargaHoraria = CheckTimingElements(timingElements);

                _driver.Close();
                _driver.SwitchTo().Window(_driver.WindowHandles.First());
                cursos.Add(curso);

            }
            _driver.Close();
            _cursoRepository.SalvaCursos(cursos);
        }

        public string CheckTeacherElements(List<string> teacherElements)
        {
            foreach (string teacherElement in teacherElements)
            {
                if (_driver.FindElements(By.ClassName(teacherElement)).Any())
                    return _driver.FindElement(By.ClassName(teacherElement)).GetAttribute("innerText");
            }

            return "Não disponível";
            
        }

        public string CheckTimingElements(List<string> timingElements)
        {
            foreach (string timingElement in timingElements)
            {
                if (_driver.FindElements(By.ClassName(timingElement)).Any())
                    return _driver.FindElement(By.ClassName(timingElement)).GetAttribute("innerText");
            }
            return "Não disponível";
        }

    }
}
