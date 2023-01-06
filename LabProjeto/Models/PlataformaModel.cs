using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace LabProjeto.Models
{
    public class PlataformaModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "É obrigatório Nome")]
        [StringLength(20, ErrorMessage = "Maximo de {0} caracteres e mínimo de {1}", MinimumLength = 1)]
        public string Nome { get; set; }
    }
}
