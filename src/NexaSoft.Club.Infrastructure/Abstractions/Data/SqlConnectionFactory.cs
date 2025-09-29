using System.Data;
using NexaSoft.Club.Application.Abstractions.Data;
using Npgsql;


namespace NexaSoft.Club.Infrastructure.Abstractions.Data;

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
