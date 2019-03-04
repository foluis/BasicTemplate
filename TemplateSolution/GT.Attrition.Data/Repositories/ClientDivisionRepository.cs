using Dapper;
using NA.Template.DataAccess.Interfaces;
using NA.Template.Entities;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace NA.Template.DataAccess.Repositories
{
    public class ClientDivisionRepository : IClientDivisionRepository
    {
        private readonly IBaseRepository _base;

        public ClientDivisionRepository(IBaseRepository baseRepo)
        {
            _base = baseRepo;
        }

        public async Task<ClientDivision> GetClientDivisionById(int id)
        {
            using (var connection = _base.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                return await connection.QueryFirstOrDefaultAsync<ClientDivision>("dbo.usp_GetClientDivision_ApiTest", parameters, null, null, CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<ClientDivision>> GetAllClientDivision()
        {
            using (var connection = _base.GetConnection())
            {
                return await connection.QueryAsync<ClientDivision>("[dbo].[usp_GetClientDivision_ApiTest_Noparams]", CommandType.StoredProcedure);
            }
        }

        public async Task<int> CreateClientDivision(ClientDivision clientDivision)
        {
            using (var connection = _base.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", clientDivision.Id, DbType.Int16);

                var result = await connection.ExecuteAsync("[dbo].[usp_GetClientDivision_ApiTest_UpdateFake]", parameters, null, null, CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
