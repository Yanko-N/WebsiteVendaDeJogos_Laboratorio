
using System.ComponentModel.DataAnnotations;

namespace LabProjeto.Models
{
    public class JogoModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "É obrigatório o jogo um Nome")]
        public string Nome { get; set; }


        [Required(ErrorMessage ="É obrigatório o jogo ter um preço")]
        public float Preco { get; set; }

        [Required(ErrorMessage ="É obrigatório o jogo ter uma categoria")]
        public CategoriaModel? categoria;

    }
}
