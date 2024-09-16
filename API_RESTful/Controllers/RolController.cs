using API_RESTful.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transferencia_Datos.Rol_DTO;


namespace API_RESTful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        // Representa la DB:
        private readonly MyDBcontext _MyDBcontext;


        // Constructor:
        public RolController(MyDBcontext myDBcontext) 
        {
            _MyDBcontext= myDBcontext;
        }




        // **************** ENDPOINTS QUE MANDARAN OBJETOS *****************
        // *****************************************************************

        // OBTIENE TODOS LOS REGISTROS DE LA DB:
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // Obtenemos Todos Los Registros:
            List<Rol> Registros_Roles = await _MyDBcontext.Roles.ToListAsync();

            // La Lista de este Objeto Retornaremos:
            Registrados_Rol_DTO Objeto_Rol = new Registrados_Rol_DTO();

            // Agregamos cada registro obtenido a la lista que retornaremos:
            foreach(Rol rol in Registros_Roles)
            {
                Objeto_Rol.Lista_Roles.Add(new Registrados_Rol_DTO.Rol
                {
                    IdRol = rol.IdRol,
                    Nombre=rol.Nombre
                });
            }

            return Ok(Objeto_Rol);
        }


        // OBTIENE UN REGISTRO CON EL MISMO ID:
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            // Obtenemos de la DB:
            Rol? Objeto_Obtenido = await _MyDBcontext.Roles.FirstOrDefaultAsync(x=> x.IdRol==id);

            if(Objeto_Obtenido!=null)
            {
                ObtenerPorID_Rol_DTO Registro_Obtenido = new ObtenerPorID_Rol_DTO 
                {
                    IdRol=Objeto_Obtenido.IdRol,
                    Nombre=Objeto_Obtenido.Nombre
                };

                return Ok(Registro_Obtenido);
            }
            else
            {
                return NotFound("Registro No Existente.");
            }

        }





        // *******  ENPOINTS QUE RECIBIRAN OBJETOS Y MODIFICARAN LA DB  *******
        // ********************************************************************

        // RECIBE UN OBJETO Y LO GUARDA EN LA DB:
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Crear_Rol_DTO crear_Rol_DTO)
        {
            // Objeto a guardar en la DB:
            Rol rol = new Rol 
            {
                Nombre=crear_Rol_DTO.Nombre
            };

            _MyDBcontext.Add(rol);
            await _MyDBcontext.SaveChangesAsync();

            return Ok("Guardado Correctamente");
        }


        // BUSCA UN REGISTRO CON EL MISMO ID EN LA DB Y LO MODIFICA
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Editar_Rol_DTO editar_Rol_DTO)
        {
            // Obtenemos de la DB:
            Rol? Objeto_Obtenido = await _MyDBcontext.Roles.FirstOrDefaultAsync(x => x.IdRol==editar_Rol_DTO.IdRol);

            if (Objeto_Obtenido != null)
            {
                Objeto_Obtenido.Nombre = editar_Rol_DTO.Nombre;

                // Actualizamos:
                _MyDBcontext.Update(Objeto_Obtenido);
                await _MyDBcontext.SaveChangesAsync();

                return Ok("Modificado Exitosamente.");
            }
            else
            {
                return NotFound("No Se Encontro El Registro.");
            }

        }


        // DELETE api/<RolController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Obtenemos de la DB:
            Rol? Objeto_Obtenido = await _MyDBcontext.Roles.FirstOrDefaultAsync(x => x.IdRol == id);

            if (Objeto_Obtenido != null)
            {
                _MyDBcontext.Remove(Objeto_Obtenido);
                await _MyDBcontext.SaveChangesAsync();

                return Ok("Eliminado Correctamente.");
            }
            else
            {
                return NotFound("No Se Encontro El Registro.");
            }

        }
    }
}
