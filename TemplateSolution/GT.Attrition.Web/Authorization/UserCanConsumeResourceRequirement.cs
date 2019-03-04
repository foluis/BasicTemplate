using NA.Template.Entities.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace NA.Template.Web.Authorization
{
    public class UserCanConsumeResourceRequirement : IAuthorizationRequirement
    {
        public CommissionModule Module { get; }
        public CommissionPermission Permission { get; }
        public string DefaultPolicyName { get; }

        public UserCanConsumeResourceRequirement(CommissionPermission permission, CommissionModule module)
        {
            Permission = permission;
            Module = module;
            DefaultPolicyName = $"UserCan{Permission}{Module}";
        }
    }
}
