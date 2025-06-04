using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;

namespace BienesSalida.Components.Pages
{
    public class BasesCon
    {
        public async Task consultaAsync()
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = "172.16.1.42",
                UserID = "usuSalidasBienes",
                Password = "2E<+AuU3v$Cx=R~7$",
                InitialCatalog = "expediente_personas2018",
                TrustServerCertificate = true // Agregar esta línea

            };

            var connectionString = builder.ConnectionString;

            try
            {
                await using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();

                Console.WriteLine("Conexión establecida correctamente.");

                var sql = "SELECT id_empleado, nombre_completo FROM vw_acceso";
                //var sql = "SELECT id_empleado, nombre_completo FROM vw_acceso WHERE RFC = '"+rfc+"' and pass = '"+pass+"'";
                await using var command = new SqlCommand(sql, connection);
                await using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    Console.WriteLine("ID: {0}, Nombre: {1}", reader.GetInt32(0), reader.GetString(1));
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Error en SQL Server: " + sqlEx.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error general: " + e.Message);
            }
        }
    }
}