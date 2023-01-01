using System.ComponentModel.DataAnnotations;

namespace LabProjeto.Models
{
    public class PerfilCategoria
    { 


        [Key]
        public int Id { get; set; }


        public int perfilId { get; set; }

        public int categoriaID { get; set; }
    }
}
