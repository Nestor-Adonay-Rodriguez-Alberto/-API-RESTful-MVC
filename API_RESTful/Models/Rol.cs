using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_RESTful.Models
{
    public class Rol
    {
        // ATRIBUTOS:
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRol { get; set; }


        [Required]
        public string Nombre { get; set; }


        // Tabla Empleado Asociada A Esta:  * RELACION *
        [JsonIgnore]
        public virtual List<Empleado>? Lista_Empleados { get; set; }
    }

}
