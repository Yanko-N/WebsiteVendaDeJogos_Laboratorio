using System.ComponentModel.DataAnnotations;

namespace LabProjeto.Models
{
    public class JogoCategoria
    {

        [Key]
        public int Id { get; set; }

        public JogoModel jogo { get; set; }
        public CategoriaModel categoria { get; set; }
    }
}
