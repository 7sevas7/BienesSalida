//using BienesSalida.Share.Models;
//using OfficeOpenXml;

//public class ReporteExcel
//{
//    public static byte[] GenerarArchivo(List<SalidasBienes> historial)
//    {
//        using var package = new ExcelPackage();
//        var ws = package.Workbook.Worksheets.Add("Formato");

//        // 🧾 Encabezados
//        ws.Cells["A1"].Value = "Nombre";
//        ws.Cells["B1"].Value = "Fecha";
//        ws.Cells["C1"].Value = "No. Salida";
//        ws.Cells["D1"].Value = "No. Inventario";
//        ws.Cells["E1"].Value = "Descripción";
//        ws.Cells["F1"].Value = "Motivo";
//        ws.Cells["G1"].Value = "Observaciones";
//        ws.Cells["H1"].Value = "Área";
//        ws.Cells["I1"].Value = "Encargado de Área";
//        ws.Cells["J1"].Value = "Estatus";

//        // 📦 Datos
//        for (int i = 0; i < historial.Count; i++)
//        {
//            var fila = i + 2; // comienza en la fila 2
//            var item = historial[i];

//            ws.Cells[$"A{fila}"].Value = item.nombre;
//            ws.Cells[$"B{fila}"].Value = item.fechaHora;
//            ws.Cells[$"C{fila}"].Value = item.noSal;
//            ws.Cells[$"D{fila}"].Value = item.noInven;
//            ws.Cells[$"E{fila}"].Value = item.descrip;
//            ws.Cells[$"F{fila}"].Value = item.motivo;
//            ws.Cells[$"G{fila}"].Value = item.observa;
//            ws.Cells[$"H{fila}"].Value = item.area;
//            ws.Cells[$"I{fila}"].Value = item.eArea;
//            ws.Cells[$"J{fila}"].Value = item.estatus;
//        }

//        return package.GetAsByteArray();
//    }
//}