using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LabProjeto.Models
{
    public class PerfilModel
    {
        [Key]
        public int Id { get; set; }

        public string utilizadorId { get; set; }
        [Required]
        public IdentityUser utilizador { get; set; }

        public float saldo { get; set; }

        public virtual ICollection<PerfilCategoria>? categoriasFavoritas { get; set; } = new List<PerfilCategoria>();

        public virtual ICollection<PerfilJogos>? jogosComprados { get; set; } = new List<PerfilJogos>();
    }
}
