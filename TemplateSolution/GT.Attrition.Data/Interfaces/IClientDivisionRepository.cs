using NA.Template.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NA.Template.DataAccess.Interfaces
{
    public interface IClientDivisionRepository
    {
        Task<ClientDivision> GetClientDivisionById(int id);
        Task<IEnumerable<ClientDivision>> GetAllClientDivision();
        Task<int> CreateClientDivision(ClientDivision clientDivision);
    }
}
