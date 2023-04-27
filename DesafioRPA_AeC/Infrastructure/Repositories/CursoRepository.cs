using DesafioRPA_AeC.Domain.Entities;
using Newtonsoft.Json;
using System.Text;

namespace DesafioRPA_AeC.Infrastructure.Repositories
{

    public interface ICursoRepository
    {
        void SalvaCursos(List<Curso> cursos, string outputPath);
    }

    internal class CursoRepository: ICursoRepository
    {
        public void SalvaCursos(List<Curso> cursos, string outputPath)
        {
            string json = JsonConvert.SerializeObject(cursos, Formatting.Indented);
            if (String.IsNullOrEmpty(outputPath)) {
                File.WriteAllText("cursos.json", json); 
            } else
            {
                File.WriteAllText(Path.Combine(outputPath,"cursos.json"), json);
            }
        }
    }
}
