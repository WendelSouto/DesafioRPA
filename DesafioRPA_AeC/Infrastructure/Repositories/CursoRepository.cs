using DesafioRPA_AeC.Domain.Entities;
using Newtonsoft.Json;
using System.Text;

namespace DesafioRPA_AeC.Infrastructure.Repositories
{

    public interface ICursoRepository
    {
        void SalvaCursos(List<Curso> cursos);
    }

    internal class CursoRepository: ICursoRepository
    {
        public void SalvaCursos(List<Curso> cursos)
        {
            string json = JsonConvert.SerializeObject(cursos, Formatting.Indented);
            File.WriteAllText("cursos.json", json);
        }
    }
}
