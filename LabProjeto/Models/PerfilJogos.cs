using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LabProjeto.Models
{
    public class PerfilJogos
    {
        [Key]
        public int Id { get; set; }

        public int perfilId { get; set; }
        public PerfilModel? perfil { get; set; }

        public int jogoId { get; set; }
        public JogoModel? jogo { get; set; }     
    }
}
