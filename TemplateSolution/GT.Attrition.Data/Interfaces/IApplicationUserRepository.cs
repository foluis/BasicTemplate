using NA.Template.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NA.Template.DataAccess.Interfaces
{
    public interface IApplicationUserRepository
    {   
        Task<int> CreateApplicationUser(ApplicationUser applicationUser, CancellationToken cancellationToken);

        Task<ApplicationUser> GetUserByNormalizedUserName(string normalizedUserName, CancellationToken cancellationToken);

        Task<ApplicationUser> GetUserByNormalizedEmail(string normalizedEmail, CancellationToken cancellationToken);

        Task<IEnumerable<string>> GetUserRoles(ApplicationUser applicationUser, CancellationToken cancellationToken);
    }
}
