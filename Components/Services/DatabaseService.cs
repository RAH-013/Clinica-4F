namespace Clinica.Components.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using MySql.Data.MySqlClient;
    using System.Reflection;

    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(IConfiguration config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config), "Se requiere IConfiguration para inicializar DatabaseService.");

            _connectionString = config.GetConnectionString("DefaultConnection")
                                ?? throw new InvalidOperationException("No se encontró la cadena de conexión 'DefaultConnection'.");
        }

        private MySqlConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public async Task<List<Dictionary<string, object>>> QueryAsync(
            string sql,
            IDictionary<string, object>? parameters = null)
        {
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentException("La consulta SQL no puede estar vacía.", nameof(sql));

            var results = new List<Dictionary<string, object>>();

            await using var connection = CreateConnection();
            await connection.OpenAsync();

            await using var command = new MySqlCommand(sql, connection);

            AddParameters(command, parameters);

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var row = new Dictionary<string, object>(reader.FieldCount);
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[reader.GetName(i)] = reader.GetValue(i);
                }

                results.Add(row);
            }

            return results;
        }

        public async Task<int> ExecuteAsync(
            string sql,
            IDictionary<string, object>? parameters = null)
        {
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentException("La instrucción SQL no puede estar vacía.", nameof(sql));

            await using var connection = CreateConnection();
            await connection.OpenAsync();

            await using var command = new MySqlCommand(sql, connection);

            AddParameters(command, parameters);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<T?> QuerySingleAsync<T>(
            string sql,
            IDictionary<string, object>? parameters = null
        ) where T : new()
        {
            var list = await QueryAsync(sql, parameters);
            if (list.Count == 0)
                return default;

            var dict = list[0];
            var obj = new T();

            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                if (dict.TryGetValue(prop.Name, out var value) && value != DBNull.Value)
                {
                    try
                    {
                        prop.SetValue(obj, Convert.ChangeType(value, prop.PropertyType));
                    }
                    catch
                    {
                        // Ignorar si no se puede convertir
                    }
                }
            }

            return obj;
        }

        private static void AddParameters(MySqlCommand command, IDictionary<string, object>? parameters)
        {
            if (parameters == null)
                return;

            foreach (var (key, value) in parameters)
            {
                var paramName = key.StartsWith('@') ? key : "@" + key;
                command.Parameters.AddWithValue(paramName, value ?? DBNull.Value);
            }
        }
    }
}
