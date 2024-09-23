using API_RESTful.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transferencia_Datos.Empleado_DTO;
using Transferencia_Datos.Rol_DTO;


namespace API_RESTful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        // Representa La DB: 
        private readonly MyDBcontext _MyDBcontext;

        // Constructor:
        public EmpleadoController(MyDBcontext myDBcontext)
        {
            _MyDBcontext = myDBcontext;
        }




        // **************** ENDPOINTS QUE MANDARAN OBJETOS *****************
        // *****************************************************************

        // OBTIENE TODOS LOS REGISTROS DE LA DB:
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // Obtenemos Todos Los Registros:
            List<Empleado> Registros_Empleados = await _MyDBcontext.Empleados
                .Include(x => x.Objeto_Rol)
                .ToListAsync();

            // La Lista de este Objeto Retornaremos:
            Registrados_Empleado_DTO Objeto_Empleado = new Registrados_Empleado_DTO();

            // Agregamos cada registro obtenido a la lista que retornaremos:
            foreach (Empleado empleado in Registros_Empleados)
            {
                Objeto_Empleado.Lista_Empleados.Add(new Registrados_Empleado_DTO.Empleado
                {
                    IdEmpleado = empleado.IdEmpleado,
                    Nombre = empleado.Nombre,
                    Salaraio = empleado.Salaraio,
                    FechaNacimiento = empleado.FechaNacimiento,
                    Email = empleado.Email,
                    Fotografia = empleado.Fotografia,
                    IdRolEnEmpleado = empleado.IdRolEnEmpleado,
                    Objeto_Rol = new Registrados_Rol_DTO.Rol
                    {
                        IdRol = empleado.Objeto_Rol.IdRol,
                        Nombre = empleado.Objeto_Rol.Nombre,
                    }
                });

            }

            return Ok(Objeto_Empleado);
        }


        // OBTIENE UN REGISTRO CON EL MISMO ID:
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            // Obtenemos de la DB:
            Empleado? Objeto_Obtenido = await _MyDBcontext.Empleados
                .Include(x => x.Objeto_Rol)
                .FirstOrDefaultAsync(x => x.IdEmpleado == id);

            if (Objeto_Obtenido != null)
            {
                // Agregamos los Datos del de la DB:
                ObtenerPorID_Empleado_DTO Registro_Obtenido = new ObtenerPorID_Empleado_DTO
                {
                    IdEmpleado = Objeto_Obtenido.IdEmpleado,
                    Nombre = Objeto_Obtenido.Nombre,
                    Salaraio = Objeto_Obtenido.Salaraio,
                    FechaNacimiento = Objeto_Obtenido.FechaNacimiento,
                    Email = Objeto_Obtenido.Email,
                    Fotografia = Objeto_Obtenido.Fotografia,
                    IdRolEnEmpleado = Objeto_Obtenido.IdRolEnEmpleado,
                    Objeto_Rol = new Registrados_Rol_DTO.Rol
                    {
                        IdRol = Objeto_Obtenido.Objeto_Rol.IdRol,
                        Nombre = Objeto_Obtenido.Objeto_Rol.Nombre
                    }
                };

                return Ok(Registro_Obtenido);

            }
            else
            {
                return NotFound("Registro No Existente.");
            }

        }


        // REGISTROS PARA VIEWDATA DE ROL:  
        [HttpGet("Lista_Roles")]
        public async Task<IActionResult> Lista_Roles()
        {
            // Obtenemos Todos Los Registros:
            List<Rol> Registros_Roles = await _MyDBcontext.Roles.ToListAsync();

            // La Lista de este Objeto Retornaremos:
            Registrados_Rol_DTO Objeto_Roles = new Registrados_Rol_DTO();

            // Agregamos cada registro obtenido a la lista que retornaremos:
            foreach (Rol rol in Registros_Roles)
            {
                Objeto_Roles.Lista_Roles.Add(new Registrados_Rol_DTO.Rol
                {
                    IdRol = rol.IdRol,
                    Nombre = rol.Nombre,
                });
            }

            return Ok(Objeto_Roles);
        }





        // *******  ENPOINTS QUE RECIBIRAN OBJETOS Y MODIFICARAN LA DB  *******
        // ********************************************************************

        // RECIBE UN OBJETO Y LO GUARDA EN LA DB:
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Crear_Empleado_DTO crear_Empleado_DTO)
        {
            // Objeto a guardar en la DB:
            Empleado empleado = new Empleado
            {
                Nombre = crear_Empleado_DTO.Nombre,
                Salaraio = crear_Empleado_DTO.Salaraio,
                FechaNacimiento = crear_Empleado_DTO.FechaNacimiento,
                Email = crear_Empleado_DTO.Email,
                Fotografia = crear_Empleado_DTO.Fotografia,
                IdRolEnEmpleado = crear_Empleado_DTO.IdRolEnEmpleado
            };

            _MyDBcontext.Add(empleado);
            await _MyDBcontext.SaveChangesAsync();

            return Ok("Guardado Correctamente");
        }


        // BUSCA UN REGISTRO CON EL MISMO ID EN LA DB Y LO MODIFICA
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Editar_Empleado_DTO editar_Empleado_DTO)
        {
            // Obtenemos de la DB:
            Empleado? Objeto_Obtenido = await _MyDBcontext.Empleados.FirstOrDefaultAsync(x => x.IdEmpleado == editar_Empleado_DTO.IdEmpleado);

            if (Objeto_Obtenido != null)
            {
                Objeto_Obtenido.Nombre = editar_Empleado_DTO.Nombre;
                Objeto_Obtenido.Salaraio = editar_Empleado_DTO.Salaraio;
                Objeto_Obtenido.FechaNacimiento = editar_Empleado_DTO.FechaNacimiento;
                Objeto_Obtenido.Email = editar_Empleado_DTO.Email;
                Objeto_Obtenido.Fotografia = editar_Empleado_DTO.Fotografia;
                Objeto_Obtenido.IdRolEnEmpleado = editar_Empleado_DTO.IdRolEnEmpleado;

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


        // OBTIENE UN REGISTRO CON EL MISMO ID Y LO ELIMINA:s
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Obtenemos de la DB:
            Empleado? Objeto_Obtenido = await _MyDBcontext.Empleados.FirstOrDefaultAsync(x => x.IdEmpleado == id);

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
