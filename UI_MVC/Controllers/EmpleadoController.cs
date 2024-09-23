using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Transferencia_Datos.Empleado_DTO;
using Transferencia_Datos.Rol_DTO;

namespace UI_MVC.Controllers
{
    public class EmpleadoController : Controller
    {
        // Para Hacer Solicitudes Al Servidor:
        private readonly HttpClient _HttpClient;


        // Constructor:
        public EmpleadoController(IHttpClientFactory httpClientFactory)
        {
            _HttpClient = httpClientFactory.CreateClient("API_RESTful");
        }
         



        // **************** ENDPOINTS QUE MANDARAN OBJETOS *****************
        // *****************************************************************

        // OBTIENE TODOS LOS REGISTROS DE LA DB:
        public async Task<IActionResult> Index()
        {
            // Solicitud GET al Endpoint de la API:
            HttpResponseMessage JSON_Obtenidos = await _HttpClient.GetAsync("/api/Empleado");

            // OBJETO:
            Registrados_Empleado_DTO Lista_Empleados = new Registrados_Empleado_DTO();

            // True=200-299
            if (JSON_Obtenidos.IsSuccessStatusCode)
            {
                // Deserializamos el Json:
                Lista_Empleados = await JSON_Obtenidos.Content.ReadFromJsonAsync<Registrados_Empleado_DTO>();
            }

            return View(Lista_Empleados);
        }


        // OBTIENE UN REGISTRO CON EL MISMO ID:
        public async Task<IActionResult> Details(int id)
        {
            // Solicitud GET al Endpoint de la API:
            HttpResponseMessage JSON_Obtenido = await _HttpClient.GetAsync("/api/Empleado/" + id);

            // OBJETO:
            ObtenerPorID_Empleado_DTO Objeto_Obtenido = new ObtenerPorID_Empleado_DTO();

            // True=200-299
            if (JSON_Obtenido.IsSuccessStatusCode)
            {
                // Deserializamos el Json:
                Objeto_Obtenido = await JSON_Obtenido.Content.ReadFromJsonAsync<ObtenerPorID_Empleado_DTO>();
            }

            return View(Objeto_Obtenido);
        }


        // OBTIENE TODOS LOS REGISTROS Rol DE LA DB Para ViewData:
        private async Task<Registrados_Rol_DTO> Lista_Roles()
        {
            // Solicitud GET al Endpoint de la API:
            HttpResponseMessage JSON_Obtenidos = await _HttpClient.GetAsync("/api/Empleado/Lista_Roles");

            // OBJETO:
            Registrados_Rol_DTO Lista_Roles = new Registrados_Rol_DTO();

            // True=200-299
            if (JSON_Obtenidos.IsSuccessStatusCode)
            {
                // Deserializamos el Json:
                Lista_Roles = await JSON_Obtenidos.Content.ReadFromJsonAsync<Registrados_Rol_DTO>();
            }


            return Lista_Roles;
        }





        // *******  ENPOINTS QUE RECIBIRAN OBJETOS Y MODIFICARAN LA DB  *******
        // ********************************************************************

        // OBTIENE LOS ROLES Y LOS MANDA EN UN VIEWDATA:
        public async Task<ActionResult> Create()
        {
            Registrados_Rol_DTO Objeto_Obtenido = await Lista_Roles();
            ViewData["Lista_Roles"] = new SelectList(Objeto_Obtenido.Lista_Roles, "IdRol", "Nombre");

            return View();
        }


        // RECIBE UN OBJETO Y LO GUARDA EN LA DB:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Crear_Empleado_DTO crear_Empleado_DTO, IFormFile Fotografia)
        {
            // Convirtiendo el archivo a Arreglo De Bytes:
            if (Fotografia != null && Fotografia.Length > 0)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    Fotografia.CopyTo(memoryStream);

                    crear_Empleado_DTO.Fotografia = memoryStream.ToArray();
                }
            }

            // Solicitud POST al Endpoint de la API:
            HttpResponseMessage Respuesta = await _HttpClient.PostAsJsonAsync("/api/Empleado", crear_Empleado_DTO);

            // True=200-299
            if (Respuesta.IsSuccessStatusCode)
            {
                // Volemos a Vista Principal:
                return RedirectToAction(nameof(Index));
            }


            ViewBag.Error = "Error al intentar guardar el registro";
            return View();
        }


        // BUSCA UN REGISTRO CON EL MISMO ID EN LA DB Y LO MANDA A VISTA
        public async Task<IActionResult> Edit(int id)
        {
            // Solicitud GET al Endpoint de la API:
            HttpResponseMessage JSON_Obtenido = await _HttpClient.GetAsync("/api/Empleado/" + id);

            // OBJETO:
            ObtenerPorID_Empleado_DTO Objeto_Obtenido = new ObtenerPorID_Empleado_DTO();

            // True=200-299
            if (JSON_Obtenido.IsSuccessStatusCode)
            {
                // Deserializamos el Json:
                Objeto_Obtenido = await JSON_Obtenido.Content.ReadFromJsonAsync<ObtenerPorID_Empleado_DTO>();
            }

            Editar_Empleado_DTO Objeto_Editar = new Editar_Empleado_DTO
            {
                IdEmpleado = Objeto_Obtenido.IdEmpleado,
                Nombre = Objeto_Obtenido.Nombre,
                Salaraio=Objeto_Obtenido.Salaraio,
                FechaNacimiento=Objeto_Obtenido.FechaNacimiento,
                Email=Objeto_Obtenido.Email,
                Fotografia=Objeto_Obtenido.Fotografia,
                IdRolEnEmpleado=Objeto_Obtenido.IdRolEnEmpleado
            };
             
            // Para Seleccionar Roles:
            Registrados_Rol_DTO Objeto_Rol = await Lista_Roles();
            ViewData["Lista_Roles"] = new SelectList(Objeto_Rol.Lista_Roles, "IdRol", "Nombre",Objeto_Editar.IdRolEnEmpleado);

            return View(Objeto_Editar);
        }


        // RECIBE EL OBJETO MODIFICADO Y LO MODIFICA EN DB:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Editar_Empleado_DTO editar_Empleado_DTO, IFormFile Fotografia)
        {
            // Solicitud GET al Endpoint de la API:
            HttpResponseMessage JSON_Obtenido = await _HttpClient.GetAsync("/api/Empleado/" + editar_Empleado_DTO.IdEmpleado);

            // OBJETO:
            ObtenerPorID_Empleado_DTO Objeto_Obtenido = new ObtenerPorID_Empleado_DTO();

            // True=200-299
            if (JSON_Obtenido.IsSuccessStatusCode)
            {
                // Deserializamos el Json:
                Objeto_Obtenido = await JSON_Obtenido.Content.ReadFromJsonAsync<ObtenerPorID_Empleado_DTO>();
            }

            // Convirtiendo el archivo a Arreglo De Bytes:
            if (Fotografia != null && Fotografia.Length > 0)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    Fotografia.CopyTo(memoryStream);

                    editar_Empleado_DTO.Fotografia = memoryStream.ToArray();
                }
            }
            else
            {
                editar_Empleado_DTO.Fotografia = Objeto_Obtenido.Fotografia;
            }

            // Solicitud PUT al Endpoint de la API:
            HttpResponseMessage Respuesta = await _HttpClient.PutAsJsonAsync("/api/Empleado", editar_Empleado_DTO);

            // True=200-299
            if (Respuesta.IsSuccessStatusCode)
            {
                // Volemos a Vista Principal:
                return RedirectToAction(nameof(Index));
            }


            ViewBag.Error = "Error al intentar Modificar el registro";
            return View();
        }


        // BUSCA UN REGISTRO CON EL MISMO ID EN LA DB Y LO MANDA A VISTA:
        public async Task<IActionResult> Delete_Vista(int id)
        {
            // Solicitud GET al Endpoint de la API:
            HttpResponseMessage JSON_Obtenido = await _HttpClient.GetAsync("/api/Empleado/" + id);

            // OBJETO:
            ObtenerPorID_Empleado_DTO Objeto_Obtenido = new ObtenerPorID_Empleado_DTO();

            // True=200-299
            if (JSON_Obtenido.IsSuccessStatusCode)
            {
                // Deserializamos el Json:
                Objeto_Obtenido = await JSON_Obtenido.Content.ReadFromJsonAsync<ObtenerPorID_Empleado_DTO>();
            }

            return View(Objeto_Obtenido);
        }


        // BUSCA UN REGISTRO CON EL MISMO ID EN LA DB Y LO ELIMINA:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ObtenerPorID_Empleado_DTO obtenerPorID_Empleado_DTO)
        {
            // Solicitud DELETE al Endpoint de la API:
            HttpResponseMessage Respuesta = await _HttpClient.DeleteAsync("/api/Empleado/" + obtenerPorID_Empleado_DTO.IdEmpleado);

            // True=200-299
            if (Respuesta.IsSuccessStatusCode)
            {
                // Volemos a Vista Principal:
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Error = "Error al intentar Eliminar el registro";
            return View();
        }


    }
}
