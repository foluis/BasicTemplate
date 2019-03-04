using NA.Template.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NA.Template.DataAccess.Interfaces
{
    public interface IAttritionRepository
    {
        Task<IEnumerable<ClientDivision>> GetAttritionByYear(int year);
    }
}
