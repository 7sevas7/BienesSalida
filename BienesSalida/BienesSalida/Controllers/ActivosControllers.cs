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
        public async Task<IActionResult> GetHistorial([FromQuery] int idUser, [FromQuery] string? fecha1, [FromQuery] string? fecha2, [FromQuery] string? nombre, [FromQuery] long? invent) {
            nombre = nombre is null ? "": nombre.Trim();
            invent = invent is null ? 0 : invent;
            var s = await BC_SistemaBienes.salidasConsGAsync(idUser, fecha1, fecha2, nombre, invent);
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
        public async Task<IActionResult> GenerarExcel([FromQuery] int idUser, [FromQuery] string? fecha1, [FromQuery] string? fecha2, [FromQuery] string? nombre, [FromQuery] long? invent)
        {
            nombre = nombre?.Trim() ?? "";
            invent ??= 0;

            var historial = await BC_SistemaBienes.salidasConsGAsync(idUser, fecha1, fecha2, nombre, invent);

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Datos");

            // 🧠 Encabezados
            var headers = new[] {"Nombre", "Fecha", "No. Salida", "No. Inventario", "Descripción","Motivo", "Observaciones", "Área", "Encargado de Área", "Estatus"};

            for (int col = 0; col < headers.Length; col++)
            {
                var cell = worksheet.Cell(1, col + 1);
                cell.Value = headers[col];
                cell.Style.Font.Bold = true;
                cell.Style.Fill.BackgroundColor = XLColor.LightGray;
                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            }

            // 📄 Datos
            int fila = 2;
            foreach (var item in historial)
            {
                worksheet.Cell(fila, 1).Value = item.nombre;
                worksheet.Cell(fila, 2).Value = item.fechaHora;
                worksheet.Cell(fila, 3).Value = item.noSal;
                worksheet.Cell(fila, 4).Value = item.noInven;
                worksheet.Cell(fila, 5).Value = item.descrip;
                worksheet.Cell(fila, 6).Value = item.motivo;
                worksheet.Cell(fila, 7).Value = item.observa;
                worksheet.Cell(fila, 8).Value = item.area;
                worksheet.Cell(fila, 9).Value = item.eArea;
                worksheet.Cell(fila, 10).Value = item.estatus;
                fila++;
            }

            // 📏 Ajustar ancho de columnas
            worksheet.Columns().AdjustToContents();

            // 📦 Generar archivo
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "reporte.xlsx");
        }
    
    }
}
