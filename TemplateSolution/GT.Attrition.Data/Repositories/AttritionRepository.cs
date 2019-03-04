using Dapper;
using NA.Template.DataAccess.Interfaces;
using NA.Template.Entities;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace NA.Template.DataAccess.Repositories
{
    public class AttritionRepository : IAttritionRepository
    {
        private readonly IBaseRepository _base;

        public AttritionRepository(IBaseRepository baseRepo)
        {
            _base = baseRepo;
        }

        public async Task<IEnumerable<ClientDivision>> GetAttritionByYear(int year)
        {
            using (var connection = _base.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@year", year);

                var appUser = await connection.QueryAsync<ClientDivision>("[dbo].[usp_GetClientDivision]", parameters, null, null, CommandType.StoredProcedure);
                return appUser;
            }
        }
    }
}
