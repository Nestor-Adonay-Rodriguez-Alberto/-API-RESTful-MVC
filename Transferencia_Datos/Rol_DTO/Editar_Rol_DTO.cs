using System.ComponentModel.DataAnnotations;

namespace Transferencia_Datos.Rol_DTO
{
    public class Editar_Rol_DTO
    {
        // ATRIBUTOS:
        [Required]
        public int IdRol { get; set; }

        [Required(ErrorMessage = "Nombre Del Rol Obligatorio.")]
        public string Nombre { get; set; }

    }
}
