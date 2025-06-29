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


        [HttpGet("historial/{idUSer}/{nombre}")]
        public async Task<IActionResult> GetHistorial(int idUser,string nombre) {
            var s = await BC_SistemaBienes.salidasConsGAsync(idUser,nombre);
            //var s = await BC_SistemaBienes.salidasConsGAsync(5912, "ANA TERESA VARGAS BARONA");
            return Ok(s);
        }


        [HttpGet]
        public IActionResult GetActivs() {
            return Ok("Mensajes de prueba ");
        }

    }
}
