using BienesSalida.Share.Models;
using Microsoft.Data.SqlClient;

namespace BienesSalida.Components.Pages
{
    public class BasesCon : IDisposable
    {
        private SqlConnection db1Users, db2SBienes;
        public int idEmp;
        public string nombreEmp;
        public Boolean res = false;

        // Constructor que inicializa las conexiones
        public BasesCon()
        {
            db1Users = new SqlConnection("Server=172.16.1.42\\COMPAC; Database=expediente_personas2018; User Id=usuSalidasBienes; Password=2E<+AuU3v$Cx=R~7$;");
            //db2SBienes = new SqlConnection("Server=192.168.1.10\\COMPAC; Database=prueba2; User Id=sa; Password=compac$1;");

            db1Users.Open();
            //db2SBienes.Open();
        }

        public async Task ObtenerUsuarios(string rfc, string pass)
        {
            string query = "SELECT id_empleado, nombre_completo FROM vw_acceso WHERE RFC = '"+rfc+"' and pass = '"+pass+"'";

            using (SqlCommand command = new SqlCommand(query, db1Users))
            using (SqlDataReader reader = await command.ExecuteReaderAsync()) // Usa versión async
            {
                if (await reader.ReadAsync()) // Solo obtiene el primer registro
                {
                    idEmp = Convert.ToInt32(reader["id_empleado"]);
                    nombreEmp = reader["nombre_completo"].ToString();
                    res = true;
                }
                else
                {
                    res = false;
                }
            }

        }

        //// Método para consultar Productos desde la segunda base de datos
        //public void ObtenerProductos()
        //{
        //    string query = "SELECT * FROM Productos";
        //    using (SqlCommand command = new SqlCommand(query, db2SBienes))
        //    using (SqlDataReader reader = command.ExecuteReader())
        //    {
        //        while (reader.Read())
        //        {
        //            Console.WriteLine($"Producto: {reader["Nombre"]}");
        //        }
        //    }
        //}

        // Liberar las conexiones correctamente
        public void Dispose()
        {
            if (db1Users != null)
            {
                db1Users.Close();
                db1Users.Dispose();
            }

            //if (db2SBienes != null)
            //{
            //    db2SBienes.Close();
            //    db2SBienes.Dispose();
            //}
        }
    }
}