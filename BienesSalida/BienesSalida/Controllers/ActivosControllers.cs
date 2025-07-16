using BienesSalida.ConexionesBD;
using BienesSalida.Share;
using BienesSalida.Share.ModelsPost;
using ClosedXML.Excel;
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
            //return Content(content, "application/json");
            return Ok(content);
        }

        [HttpGet]
        public IActionResult GetActivs() {
            return Ok("Mensajes de prueba ");
        }

        [HttpGet("generar")]
        public IActionResult GenerarExcel()
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Datos");

            worksheet.Cell(1, 1).Value = "Nombre";
            worksheet.Cell(1, 2).Value = "Fecha";
            worksheet.Cell(1, 3).Value = "No. Salida";
            worksheet.Cell(1, 4).Value = "No. Inventario";
            worksheet.Cell(1, 5).Value = "Descripción";
            worksheet.Cell(1, 6).Value = "Motivo";
            worksheet.Cell(1, 7).Value = "Observaciones";
            worksheet.Cell(1, 8).Value = "Área";
            worksheet.Cell(1, 9).Value = "Encargado de Área";
            worksheet.Cell(1, 10).Value = "Estatus";

            //worksheet.Cell(2, 1).Value = 1;
            //worksheet.Cell(2, 2).Value = "Sebastián";

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "reporte.xlsx");
        }
    }
}
