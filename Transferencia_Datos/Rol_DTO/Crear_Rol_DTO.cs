using System.ComponentModel.DataAnnotations;

namespace Transferencia_Datos.Rol_DTO
{
    public class Crear_Rol_DTO
    {
        // ATRIBUTOS:
        [Required(ErrorMessage = "Nombre Del Rol Obligatorio.")]
        public string Nombre { get; set; }

    }
}
