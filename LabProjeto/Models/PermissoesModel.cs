using System.ComponentModel.DataAnnotations;

namespace LabProjeto.Models
{
    public class PermissoesModel
    {

        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }
    }

}
