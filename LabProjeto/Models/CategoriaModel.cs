using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace LabProjeto.Models
{
    public class CategoriaModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "É obrigatório Nome")]
        [StringLength(50, ErrorMessage = "Maximo de {0} caracteres e mínimo de {1}", MinimumLength = 3)]
        public string Nome { get; set; }

        public string Descricao { get; set; }


        [RegularExpression(@"^.+\.([jJ][pP][gG]|[pP][nN][gG])$", ErrorMessage = "Só jpg e png files")]

        public string? Foto { get; set; }
    }
}
