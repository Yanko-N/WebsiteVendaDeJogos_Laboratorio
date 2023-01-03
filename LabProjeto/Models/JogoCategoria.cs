using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace LabProjeto.Models
{
    public class JogoCategoria
    {

        [Key]
        public int Id { get; set; }

        public int jogoId { get; set; }
        public JogoModel? jogo { get; set; }
       

        public int categoriaId { get; set; }
        public CategoriaModel? categoria { get; set; }
    }
}
