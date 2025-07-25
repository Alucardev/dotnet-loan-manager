// Importación de espacios de nombres necesarios
using System.Data; // Para trabajar con interfaces de conexión de base de datos
using LoanManager.Application.Abstractions.Data; // Interfaz ISqlConnectionFactory
using Npgsql; // Cliente para conectarse a bases de datos PostgreSQL

// Definición de la clase SqlConnectionFactory que implementa la interfaz ISqlConnectionFactory
internal sealed class SqlConnectionFactory : ISqlConnectionFactory
{
    // Campo privado que almacena la cadena de conexión
    private readonly string _connectionString;

    // Constructor que recibe la cadena de conexión y la inicializa
    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString; // Asigna la cadena de conexión al campo privado
    }

    // Método que crea y devuelve una nueva conexión a la base de datos
    public IDbConnection CreateConnection()
    {
        // Crea una nueva instancia de NpgsqlConnection utilizando la cadena de conexión
        var connection = new NpgsqlConnection(_connectionString);

        // Abre la conexión a la base de datos
        connection.Open();

        // Devuelve la conexión abierta
        return connection;
    }
}
