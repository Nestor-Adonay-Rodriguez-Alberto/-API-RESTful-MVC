using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_RESTful.Models
{
    public class Empleado
    {
        // ATRIBUTOS:
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEmpleado { get; set; }


        [Required]
        public string Nombre { get; set; }


        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public double Salaraio { get; set; }


        [Required]
        public DateTime FechaNacimiento { get; set; }


        [Required]
        public string Email { get; set; }


        public byte[]? Fotografia { get; set; }


        // Referencia Tabla Empleado:  * RELACION *
        [Required]
        [ForeignKey("Objeto_Rol")]
        public int IdRolEnEmpleado { get; set; }

        [JsonIgnore]
        public virtual Rol? Objeto_Rol { get; set; }
    }
}
