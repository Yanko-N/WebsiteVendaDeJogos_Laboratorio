using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LabProjeto.Models
{
    public class Pontuacao
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="é Requerido um utilizador")]
        public string? utilizadorId { get; set; }

        public IdentityUser Utilizador { get; set; }

        [Display(Name ="Pontuacao")]
        [Required(ErrorMessage = "É requerido Pontuar o jogo")]
        [Range(0,10,ErrorMessage ="A pontuação vai de 0 a 10")]
        public int pontuacao { get; set; }

        public int? JogoId { get; set; }

        public JogoModel Jogo { get; set; }
    }
}
