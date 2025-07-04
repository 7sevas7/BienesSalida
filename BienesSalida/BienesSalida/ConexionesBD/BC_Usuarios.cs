﻿using Microsoft.Data.SqlClient;

namespace BienesSalida.ConexionesBD
{
   
    public class BC_Usuarios
    {
        
        public bool res = false;
        public string nameEmp;
        public int idEmp;
        public async Task<bool> consultaAsync(string rfc, string pass)
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

                var sql = "EXEC ConsultaSistemaDeBienes @rfc, @pass";
                await using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@rfc", rfc);
                command.Parameters.AddWithValue("@pass", pass);

                await using var reader = await command.ExecuteReaderAsync();

                // Verificar si hay resultados
                if (await reader.ReadAsync())
                {
                    res = true;

                    idEmp = reader.GetInt32(0);
                    nameEmp = reader.GetString(1);
                    return true;
                }
                else
                {
                    res = false;
                    return false;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Error en SQL Server: " + sqlEx.Message);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error general: " + e.Message);
                return true;
            }
        }
    }
}