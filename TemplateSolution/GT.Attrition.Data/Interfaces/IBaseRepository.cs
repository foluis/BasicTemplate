using System.Data;

namespace NA.Template.DataAccess.Interfaces
{
    public interface IBaseRepository
    {
        string ConnectionString { get; }

        IDbConnection GetConnection();

        IDbConnection GetConnection2();
    }
}
