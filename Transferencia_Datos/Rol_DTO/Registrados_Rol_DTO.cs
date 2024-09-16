using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transferencia_Datos.Rol_DTO
{
    public class Registrados_Rol_DTO
    {
        // CLASE:
        public class Rol
        {
            public int IdRol { get; set; }

            public string Nombre { get; set; }
        }


        // REGISTROS EN LA DB:
        public List<Rol> Lista_Roles { get; set; }


        // CONSTRUCTOR:
        public Registrados_Rol_DTO()
        {
            Lista_Roles = new List<Rol>();
        }
    }
}
