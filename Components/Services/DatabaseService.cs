namespace Clinica.Components.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using MySql.Data.MySqlClient;

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

        /// <summary>
        /// Ejecuta una consulta SELECT y devuelve una lista de filas como diccionarios.
        /// </summary>
        public async Task<List<Dictionary<string, object>>> QueryAsync(
            string sql,
            IDictionary<string, object>? parametros = null)
        {
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentException("La consulta SQL no puede estar vacía.", nameof(sql));

            var resultados = new List<Dictionary<string, object>>();

            await using var conexion = CreateConnection();
            await conexion.OpenAsync();

            await using var comando = new MySqlCommand(sql, conexion);
            AddParameters(comando, parametros);

            await using var lector = await comando.ExecuteReaderAsync();
            while (await lector.ReadAsync())
            {
                var fila = new Dictionary<string, object>(lector.FieldCount);
                for (int i = 0; i < lector.FieldCount; i++)
                {
                    fila[lector.GetName(i)] = lector.GetValue(i);
                }

                resultados.Add(fila);
            }

            return resultados;
        }

        /// <summary>
        /// Ejecuta una instrucción INSERT, UPDATE o DELETE y devuelve el número de filas afectadas.
        /// </summary>
        public async Task<int> ExecuteAsync(
            string sql,
            IDictionary<string, object>? parametros = null)
        {
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentException("La instrucción SQL no puede estar vacía.", nameof(sql));

            await using var conexion = CreateConnection();
            await conexion.OpenAsync();

            await using var comando = new MySqlCommand(sql, conexion);
            AddParameters(comando, parametros);

            return await comando.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Agrega parámetros al comando SQL si están definidos.
        /// </summary>
        private static void AddParameters(MySqlCommand comando, IDictionary<string, object>? parametros)
        {
            if (parametros == null)
                return;

            foreach (var (clave, valor) in parametros)
            {
                var nombreParametro = clave.StartsWith('@') ? clave : "@" + clave;
                comando.Parameters.AddWithValue(nombreParametro, valor ?? DBNull.Value);
            }
        }
    }
}
