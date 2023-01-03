using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace LabProjeto.Models
{
    public class PerfilCategoria
    { 


        [Key]
        public int Id { get; set; }


        public int perfilId { get; set; }
        public PerfilModel? perfil { get; set; }

        public int categoriaId { get; set; }
        public CategoriaModel? categoria { get; set; }

    }
}
