using NA.Template.DataAccess.Interfaces;
using NA.Template.Entities;
using System.Data;
using System.Data.SqlClient;

namespace NA.Template.DataAccess.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        public string ConnectionString { get; }
        public string ConnectionString2 { get; }

        public BaseRepository(CustomSettings customConfig)
        {
            ConnectionString = customConfig.ConnectionString;
            ConnectionString2 = customConfig.ConnectionString2;
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public IDbConnection GetConnection2()
        {
            return new SqlConnection(ConnectionString2);
        }
    }
}
