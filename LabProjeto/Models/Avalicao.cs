using System.ComponentModel.DataAnnotations;

namespace LabProjeto.Models
{
    public class Avalicao
    {
        [Key]
        public int Id { get; set; }

        public int JogoId { get; set; }
        public JogoModel? Jogo { get; set; }

        [Required(ErrorMessage = "É necessário um Autor")]
        public string Name { get; set; }

        [Required(ErrorMessage = "É necessário uma avaliacao")]
        [Range(0,10,ErrorMessage="Vai de 0 a 10 a avaliação")]
        public int pontuacao { get; set; }
    }
}
