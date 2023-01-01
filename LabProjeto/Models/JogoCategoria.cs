using System.ComponentModel.DataAnnotations;

namespace LabProjeto.Models
{
    public class JogoCategoria
    {

        [Key]
        public int Id { get; set; }

        public int jogoId { get; set; }

        public int categoriaID { get; set; }
    }
}
