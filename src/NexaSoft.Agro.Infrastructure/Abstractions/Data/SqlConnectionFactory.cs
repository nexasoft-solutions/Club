using System.Data;
using NexaSoft.Agro.Application.Abstractions.Data;
using Npgsql;


namespace NexaSoft.Agro.Infrastructure.Abstractions.Data;

internal sealed class SqlConnectionFactory : ISqlConnectionFactory
{
   private readonly string _connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        return connection;
    }
}
