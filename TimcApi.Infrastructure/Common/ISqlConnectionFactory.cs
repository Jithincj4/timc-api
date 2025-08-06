using System.Data;

namespace TimcApi.Infrastructure.Common
{
    public interface ISqlConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
