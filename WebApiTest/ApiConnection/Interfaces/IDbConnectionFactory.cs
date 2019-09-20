
using System.Data;

namespace ApiConnection.Interfaces
{
    public interface IDbConnectionFactory
    {
        IDbConnection Create();
        IDbConnection Create(string connectionString, string provider = "System.Data.SqlClient");
    }
}
