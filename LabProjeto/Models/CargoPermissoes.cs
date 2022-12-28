using System.ComponentModel.DataAnnotations;

namespace LabProjeto.Models

{
    public class CargoPermissoes
    {
        [Key]
        public int Id { get; set; }

        public PermissoesModel permissao { get; set; }
        public CargoModel cargo { get; set; }

    }
}
