using BienesSalida.ConexionesBD;
using BienesSalida.Share;
using BienesSalida.Share.ModelsPost;

using Microsoft.AspNetCore.Mvc;

namespace BienesSalida.Controllers
{
    [ApiController]
    [Route("api/activo")]
    public class ActivosControllers : Controller
    {
        private BC_SistemaBienes BC_SistemaBienes { set; get; }

        public ActivosControllers()
        {
            BC_SistemaBienes = new();
        }

        [HttpPost]
        public async Task<IActionResult> SetActivo([FromBody] InsertSolicitud body) {
            var nActivos = body.activos.Count;
            try
            {
                for (int i = 0; i < nActivos; i++)
                {
                    var activo = body.activos[i];
                    await BC_SistemaBienes.salidasInserAsync(
                       body.idUser,
                       activo.Fecha,
                       activo.Nombre,
                       activo.NumeroInventario,
                       activo.Descripcion,
                       activo.MotivoSalida,
                       activo.Observaciones,
                       activo.Area,
                       activo.EncargadoArea,
                       activo.estatus
                    );
                }
                return Ok();
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseApi { tipe = TipoRes.Existe,response= ex.Message });
                //return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }


        [HttpGet("historial")]
        public async Task<IActionResult> GetHistorial([FromQuery] int idUser, [FromQuery] string? fecha, [FromQuery] string? nombre, [FromQuery] long? invent) {
            nombre = nombre is null ? "": nombre.Trim();
            invent = invent is null ? 0 : invent;
            var s = await BC_SistemaBienes.salidasConsGAsync(idUser, fecha, nombre, invent);
            //var s = await BC_SistemaBienes.salidasConsGAsync(5912, "ANA TERESA VARGAS BARONA");
            return Ok(s);
        }

        // En tu backend
        [HttpGet("proxy")]
        public async Task<IActionResult> ProxyToExternalApi([FromQuery] long? invent)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"https://sistemas.dif.hidalgo.gob.mx/WebServicesARM/ActivoFijo/api/Bienes/consulta?bienID={invent}&pagina=1");
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        [HttpGet]
        public IActionResult GetActivs() {
            return Ok("Mensajes de prueba ");
        }

    }
}
