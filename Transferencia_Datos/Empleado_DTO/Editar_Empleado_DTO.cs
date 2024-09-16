using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Transferencia_Datos.Rol_DTO.Registrados_Rol_DTO;

namespace Transferencia_Datos.Empleado_DTO
{
    public class Editar_Empleado_DTO
    {
        // ATRIBUTOS:
        [Required]
        public int IdEmpleado { get; set; }


        [Required(ErrorMessage = "Ingrese El Nombre Del Empleado.")]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "Salario Mensual.")]
        public double Salaraio { get; set; }


        [Required(ErrorMessage = "Ingrese La Fecha De Nacimiento.")]
        public DateTime FechaNacimiento { get; set; }


        [Required(ErrorMessage = "Ingrese Un Gmail.")]
        public string Email { get; set; }


        // Referencia Tabla Empleado:  * RELACION *
        [Required(ErrorMessage = "Ingrese El Rol Del Empleado.")]
        public int IdRolEnEmpleado { get; set; }

        public virtual Rol? Objeto_Rol { get; set; }


    }
}
