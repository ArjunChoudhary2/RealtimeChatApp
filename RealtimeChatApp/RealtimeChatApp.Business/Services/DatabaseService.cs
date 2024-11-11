using Npgsql;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace RealtimeChatApp.RealtimeChatApp.Business.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        // Inject IConfiguration to access appsettings.json for the connection string
        public DatabaseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SupabaseConnection");
        }

        public async Task GetUserDataAsync()
        {
            try
            {
                // Open a connection and execute a command
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Define and execute the query
                    var query = "SELECT * FROM users LIMIT 1";  // Example query
                    using var command = new NpgsqlCommand(query, connection);
                    using var reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        // Example to process data
                        Console.WriteLine(reader["id"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error details: {ex.Message}");
                throw;
            }
        }
    }
}
