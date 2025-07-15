using OfficeOpenXml;

namespace BienesSalida.Client.Pages
{
    public class ReporteExcel
    {
        public ReporteExcel()
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Reporte");
                worksheet.Cells["A1"].Value = "Producto";
                worksheet.Cells["B1"].Value = "Cantidad";
                worksheet.Cells["C1"].Value = "Precio";

                worksheet.Cells["A2"].Value = "Producto A";
                worksheet.Cells["B2"].Value = 10;
                worksheet.Cells["C2"].Value = 25.00;

                var fileBytes = package.GetAsByteArray();
                //await FileUtil.SaveAs("reporte.xlsx", fileBytes);
            }
    }
}
