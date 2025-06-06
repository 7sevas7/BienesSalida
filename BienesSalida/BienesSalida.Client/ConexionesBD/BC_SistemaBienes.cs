using Microsoft.Data.SqlClient;

namespace BienesSalida.Client.ConexionesBD
{
    public class BC_SistemaBienes
    {
        public async Task consultaAsync(int idUserEU, string Nombre, string Roll)
        {
            var builder = new SqlConnectionStringBuilder
            {
                //DataSource = "localhost", //Va la IP del server
                //UserID = "",
                //Password = "",
                //InitialCatalog = "SistemaBien",
                //TrustServerCertificate = true

                //-------------- CONEXIÓN DE MANERA LOCAL -------------------
                DataSource = "localhost",  // IP del servidor
                InitialCatalog = "SistemaBien",
                IntegratedSecurity = true, // ✅ Habilita autenticación de Windows
                TrustServerCertificate = true

            };

            var connectionString = builder.ConnectionString;

            try
            {
                await using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();
                Console.WriteLine("Conexión establecida correctamente.");

                //USUARIOS
                var sql = "EXEC InsercLogin @ID_UserEU, @Nombre, @Roll";
                await using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ID_UserEU", idUserEU);
                command.Parameters.AddWithValue("@Nombre", Nombre);
                command.Parameters.AddWithValue("@Roll", Roll);

                await using var reader = await command.ExecuteReaderAsync();
                await reader.ReadAsync();

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Error en SQL Server2: " + sqlEx.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error general2: " + e.Message);
            }
        }

    }
}
