using System.ComponentModel.DataAnnotations;

namespace LabProjeto.Models
{
    public class CargoModel
    {

        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }
    }
}
