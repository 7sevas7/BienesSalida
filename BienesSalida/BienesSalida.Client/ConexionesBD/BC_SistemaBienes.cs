using Microsoft.Data.SqlClient;

namespace BienesSalida.Client.ConexionesBD
{
    public class BC_SistemaBienes
    {
        private readonly SqlConnection connection;

        public BC_SistemaBienes()
        {
            var builder = new SqlConnectionStringBuilder
            {
                //DataSource = "localhost", //Va la IP del server
                //UserID = "",
                //Password = "",
                //InitialCatalog = "SistemaBien",
                //TrustServerCertificate = true

                //-------------- CONEXIÓN DE MANERA LOCAL -------------------
                DataSource = "localhost",
                InitialCatalog = "SistemaBien",
                IntegratedSecurity = true, // ✅ Habilita autenticación de Windows
                TrustServerCertificate = true
            };

            var connectionString = builder.ConnectionString;
            connection = new SqlConnection(connectionString); //Inicializa la conexión en el constructor
        }

        //--------------------------- USUARIOS ---------------------------
        public async Task consultaAsync(int idUserEU, string Nombre, string Roll)
        {
            try
            {
                await connection.OpenAsync(); //Abre la conexión antes de ejecutar la consulta
                Console.WriteLine("Conexión establecida correctamente.");

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

        //--------------------------- SALIDAS ---------------------------
        public async Task salidasAsync(int idUserEU, string FyH, string Nombre, int nSal, int nInv, string descrip, string moti, string obser, 
                                       string area, string encArea, string estatus)
        {
            try
            {
                await connection.OpenAsync();
                Console.WriteLine("Conexión establecida correctamente.");

                //USUARIOS
                var sql = "EXEC InsercSalida @ID_UserEU, @FyH, @Nombre, @nSal, @nInv, @descrip, @moti, @obser, @area, @encArea, @estatus";
                await using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ID_UserEU", idUserEU);
                command.Parameters.AddWithValue("@FyH", FyH);
                command.Parameters.AddWithValue("@Nombre", Nombre);
                command.Parameters.AddWithValue("@nSal", nSal);
                command.Parameters.AddWithValue("@nInv", nInv);
                command.Parameters.AddWithValue("@descrip", descrip);
                command.Parameters.AddWithValue("@moti", moti);
                command.Parameters.AddWithValue("@obser", obser);
                command.Parameters.AddWithValue("@area", area);
                command.Parameters.AddWithValue("@encArea", encArea);
                command.Parameters.AddWithValue("@estatus", estatus);

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