using Dapper;
using NA.Template.DataAccess.Interfaces;
using NA.Template.Entities;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace NA.Template.DataAccess.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly IBaseRepository _base;

        public ApplicationUserRepository(IBaseRepository baseRepo)
        {
            _base = baseRepo;
        }

        public async Task<int> CreateApplicationUser(ApplicationUser user, CancellationToken cancellationToken)
        {
            using (var connection = _base.GetConnection2())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", 1, DbType.Int16);

                var result = await connection.QuerySingleAsync<int>($@"INSERT INTO [ApplicationUser] ([UserName], [NormalizedUserName], [Email],
                    [NormalizedEmail], [EmailConfirmed], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled])
                    VALUES (@{nameof(ApplicationUser.UserName)}, @{nameof(ApplicationUser.NormalizedUserName)}, @{nameof(ApplicationUser.Email)},
                    @{nameof(ApplicationUser.NormalizedEmail)}, @{nameof(ApplicationUser.EmailConfirmed)}, @{nameof(ApplicationUser.PasswordHash)},
                    @{nameof(ApplicationUser.PhoneNumber)}, @{nameof(ApplicationUser.PhoneNumberConfirmed)}, @{nameof(ApplicationUser.TwoFactorEnabled)});
                    SELECT CAST(SCOPE_IDENTITY() as int)", user);

                return result;
            }
        }

        public async Task<ApplicationUser> GetUserByNormalizedUserName(string normalizedUserName, CancellationToken cancellationToken)
        {
            var result = new ApplicationUser();

            using (var connection = _base.GetConnection2())
            {
                //var parameters = new DynamicParameters();
                //parameters.Add("@NormalizedUserName", normalizedUserName);

                var user = await connection.QuerySingleOrDefaultAsync<ApplicationUser>($@"SELECT * FROM [ApplicationUser]
                    WHERE [NormalizedUserName] = @{nameof(normalizedUserName)}", new { normalizedUserName });

                var permissions = await connection.QueryAsync<Permission>($@"SELECT p.Id,p.Name
                    FROM[Permission] p
                    INNER JOIN ApplicationRolePermission arp ON arp.PermissionId = p.Id
                    INNER JOIN ApplicationUserRole aur ON aur.RoleId = arp.ApplicationRoleId
                    INNER JOIN ApplicationUser au ON au.Id = aur.UserId
                    WHERE au.NormalizedEmail  = @{nameof(normalizedUserName)}", new { normalizedUserName });

                user.Permissions = permissions;
                result = user;
            }

            return result;
        }

        public async Task<ApplicationUser> GetUserByNormalizedEmail(string normalizedEmail, CancellationToken cancellationToken)
        {
            var result = new ApplicationUser();

            using (var connection = _base.GetConnection2())
            {
                var user = await connection.QuerySingleOrDefaultAsync<ApplicationUser>($@"SELECT * FROM [ApplicationUser]
                    WHERE [NormalizedUserName] = @{nameof(normalizedEmail)}", new { normalizedEmail });

                var permissions = await connection.QueryAsync<List<Permission>>($@"SELECT p.Id,p.Name
                    FROM[Permission] p
                    INNER JOIN ApplicationRolePermission arp ON arp.PermissionId = p.Id
                    INNER JOIN ApplicationUserRole aur ON aur.RoleId = arp.ApplicationRoleId
                    INNER JOIN ApplicationUser au ON au.Id = aur.UserId
                    WHERE au.NormalizedEmail  = @{nameof(normalizedEmail)}", new { normalizedEmail });
            }
            return result;
        }

        public async Task<IEnumerable<string>> GetUserRoles(ApplicationUser user, CancellationToken cancellationToken)
        {
            //using (var connection = new SqlConnection(_connectionString))
            //{
            //    await connection.OpenAsync(cancellationToken);
            //    var queryResults = await connection.QueryAsync<string>("SELECT r.[Name] FROM [ApplicationRole] r INNER JOIN [ApplicationUserRole] ur ON ur.[RoleId] = r.Id " +
            //        "WHERE ur.UserId = @userId", new { userId = user.Id });

            //    return queryResults.ToList();
            //}

            using (var connection = _base.GetConnection2())
            {
                //var parameters = new DynamicParameters();
                //parameters.Add("@NormalizedUserName", normalizedUserName);

                var result = await connection.QueryAsync<string>("SELECT r.[Name] FROM [ApplicationRole] r INNER JOIN [ApplicationUserRole] ur ON ur.[RoleId] = r.Id " +
                    "WHERE ur.UserId = @userId", new { userId = user.Id });

                return result;
            }
        }
    }
}
