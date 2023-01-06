
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace LabProjeto.Models
{
    public class JogoModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "É obrigatório o jogo ter um Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "É obrigatório o jogo ter um preço")]

        public float Preco { get; set; }

        public int plataformaId { get; set; }
        public PlataformaModel? plataforma { get; set; }

        public int categoriaId { get; set; }
        public  CategoriaModel? categoria { get; set; }

    }
}
