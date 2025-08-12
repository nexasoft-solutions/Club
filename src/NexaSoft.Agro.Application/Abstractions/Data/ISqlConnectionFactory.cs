using System.Data;

namespace NexaSoft.Agro.Application.Abstractions.Data;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}