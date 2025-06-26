using Microsoft.Data.SqlClient;

namespace BienesSalida.ConexionesBD
{
    public class BC_SistemaBienes
    {
        private readonly SqlConnection connection;

        public BC_SistemaBienes()
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = "172.16.9.10", //Va la IP del server
                UserID = "usControlSalidaBienes",
                Password = "brf_Ba902CU-W2SSS#eX",
                InitialCatalog = "ControlSalidaBienes",
                TrustServerCertificate = true

                ////-------------- CONEXIÓN DE MANERA LOCAL -------------------
                //DataSource = "localhost",
                //InitialCatalog = "SistemaBien",
                //IntegratedSecurity = true, // ✅ Habilita autenticación de Windows
                //TrustServerCertificate = true
            };

            var connectionString = builder.ConnectionString;
            connection = new SqlConnection(connectionString); //Inicializa la conexión en el constructor
        }

        //--------------------------- USUARIOS ---------------------------
        public async Task usuariosAsync(int idUserEU, string Nombre, string Roll)
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
        public async Task salidasInserAsync(int idUserEU, string FyH, string Nombre, long? nInv, string? descrip, string? moti, string? obser, 
                                       string? area, string? encArea, string? estatus)
        {
            try
            {
                await connection.OpenAsync();
                Console.WriteLine("Conexión establecida correctamente.");

                //USUARIOS
                var sql = "EXEC InsercSalida @ID_UserEU, @FyH, @Nombre, @nInv, @descrip, @moti, @obser, @area, @encArea, @estatus";
                await using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ID_UserEU", idUserEU);
                command.Parameters.AddWithValue("@FyH", FyH);
                command.Parameters.AddWithValue("@Nombre", Nombre);
                command.Parameters.AddWithValue("@nInv", nInv);
                command.Parameters.AddWithValue("@descrip", descrip);
                command.Parameters.AddWithValue("@moti", moti);
                command.Parameters.AddWithValue("@obser", obser);
                command.Parameters.AddWithValue("@area", area);
                command.Parameters.AddWithValue("@encArea", encArea);
                command.Parameters.AddWithValue("@estatus", estatus);

                await using var reader = await command.ExecuteReaderAsync();

                while (reader.Read())
                {
                    if(reader["Resultado"].ToString() == "0"){
                        throw new Exception("Articulo no disponible");
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Error en SQL Server3: " + sqlEx.Message);
                throw new Exception(sqlEx.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error general3: " + e.Message);
                throw new Exception(e.Message);
            }
        }

        public async Task salidasConsGAsync(int idUserEU, string Nombre)
        {
            try
            {
                await connection.OpenAsync();
                Console.WriteLine("Conexión establecida correctamente.");
                string sql;
                sql = "EXECT ObtenerSalida @idUserEU, @Nombre";
                await using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@idUserEU", idUserEU);
                command.Parameters.AddWithValue("@Nombre", Nombre);

                await using var reader = await command.ExecuteReaderAsync();
                await reader.ReadAsync();
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Error en SQL Server4: " + sqlEx.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error general4: " + e.Message);
            }
        }
    }
}