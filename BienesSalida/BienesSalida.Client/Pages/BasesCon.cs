using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using System.Threading.Tasks;

namespace BienesSalida.Components.Pages
{
    public class BasesCon
    {
        public static Boolean res = false;

        public async Task consultaAsync(string rfc, string pass)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = "172.16.1.42",
                UserID = "usuSalidasBienes",
                Password = "2E<+AuU3v$Cx=R~7$",
                InitialCatalog = "expediente_personas2018",
                TrustServerCertificate = true
            };

            var connectionString = builder.ConnectionString;

            try
            {
                await using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();

                Console.WriteLine("Conexión establecida correctamente.");

                var sql = "SELECT id_empleado, nombre_completo FROM vw_acceso WHERE RFC = @rfc AND pass = @pass";
                await using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@rfc", rfc);
                command.Parameters.AddWithValue("@pass", pass);

                await using var reader = await command.ExecuteReaderAsync();

                // Verificar si hay resultados
                if (await reader.ReadAsync())
                {
                    res = true;
                }
                else
                {
                    res = false;
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