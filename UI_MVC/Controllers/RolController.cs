using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Transferencia_Datos.Rol_DTO;

namespace UI_MVC.Controllers
{
    public class RolController : Controller
    {
        // Para Hacer Solicitudes Al Servidor:
        private readonly HttpClient _HttpClient;


        // Constructor:
        public RolController(IHttpClientFactory httpClientFactory)
        {
            _HttpClient = httpClientFactory.CreateClient("API_RESTful");
        }




        // **************** ENDPOINTS QUE MANDARAN OBJETOS *****************
        // *****************************************************************

        // OBTIENE TODOS LOS REGISTROS DE LA DB:
        public async Task<IActionResult> Index()
        {
            // Solicitud GET al Endpoint de la API:
            HttpResponseMessage JSON_Obtenidos = await _HttpClient.GetAsync("/api/Rol");

            // OBJETO:
            Registrados_Rol_DTO Lista_Rols = new Registrados_Rol_DTO();

            // True=200-299
            if (JSON_Obtenidos.IsSuccessStatusCode)
            {
                // Deserializamos el Json:
                Lista_Rols = await JSON_Obtenidos.Content.ReadFromJsonAsync<Registrados_Rol_DTO>();
            }


            return View(Lista_Rols);
        }


        // OBTIENE UN REGISTRO CON EL MISMO ID:
        public async Task<IActionResult> Details(int id)
        {
            // Solicitud GET al Endpoint de la API:
            HttpResponseMessage JSON_Obtenido = await _HttpClient.GetAsync("/api/Rol/" + id);

            // OBJETO:
            ObtenerPorID_Rol_DTO Objeto_Obtenido = new ObtenerPorID_Rol_DTO();

            // True=200-299
            if (JSON_Obtenido.IsSuccessStatusCode)
            {
                // Deserializamos el Json:
                Objeto_Obtenido = await JSON_Obtenido.Content.ReadFromJsonAsync<ObtenerPorID_Rol_DTO>();
            }

            return View(Objeto_Obtenido);
        }




        // *******  ENPOINTS QUE RECIBIRAN OBJETOS Y MODIFICARAN LA DB  *******
        // ********************************************************************

        // NOS MANDA A LA VISTA:
        public ActionResult Create()
        {
            return View();
        }

        // RECIBE UN OBJETO Y LO GUARDA EN LA DB:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Crear_Rol_DTO crear_Rol_DTO)
        {
            // Solicitud POST al Endpoint de la API:
            HttpResponseMessage Respuesta = await _HttpClient.PostAsJsonAsync("/api/Rol", crear_Rol_DTO);

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
            HttpResponseMessage JSON_Obtenido = await _HttpClient.GetAsync("/api/Rol/" + id);

            // OBJETO:
            ObtenerPorID_Rol_DTO Objeto_Obtenido = new ObtenerPorID_Rol_DTO();

            // True=200-299
            if (JSON_Obtenido.IsSuccessStatusCode)
            {
                // Deserializamos el Json:
                Objeto_Obtenido = await JSON_Obtenido.Content.ReadFromJsonAsync<ObtenerPorID_Rol_DTO>();
            }

            Editar_Rol_DTO Objeto_Editar = new Editar_Rol_DTO
            {
                Nombre = Objeto_Obtenido.Nombre,
            };

            return View(Objeto_Editar);
        }


        // RECIBE EL OBJETO MODIFICADO Y LO MODIFICA EN DB:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Editar_Rol_DTO editar_Rol_DTO)
        {
            // Solicitud PUT al Endpoint de la API:
            HttpResponseMessage Respuesta = await _HttpClient.PutAsJsonAsync("/api/Rol", editar_Rol_DTO);

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
            HttpResponseMessage JSON_Obtenido = await _HttpClient.GetAsync("/api/Rol/" + id);

            // OBJETO:
            ObtenerPorID_Rol_DTO Objeto_Obtenido = new ObtenerPorID_Rol_DTO();

            // True=200-299
            if (JSON_Obtenido.IsSuccessStatusCode)
            {
                // Deserializamos el Json:
                Objeto_Obtenido = await JSON_Obtenido.Content.ReadFromJsonAsync<ObtenerPorID_Rol_DTO>();
            }

            return View(Objeto_Obtenido);
        }


        // BUSCA UN REGISTRO CON EL MISMO ID EN LA DB Y LO ELIMINA:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ObtenerPorID_Rol_DTO obtenerPorID_Rol_DTO)
        {
            // Solicitud GET al Endpoint de la API:
            HttpResponseMessage Respuesta = await _HttpClient.DeleteAsync("/api/Rol/" + obtenerPorID_Rol_DTO.IdRol);

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
