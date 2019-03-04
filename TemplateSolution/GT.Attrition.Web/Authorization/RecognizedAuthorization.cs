using NA.Template.Entities.Authorization;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static System.Array;

namespace NA.Template.Web.Authorization
{
    public static class RecognizedAuthorization
    {
        public static ReadOnlyCollection<CommissionModule> Modules { get; }
        public static ReadOnlyCollection<CommissionPermission> Permissions { get; }

        static RecognizedAuthorization()
        {
            Modules = AsReadOnly(new[] {
                     CommissionModule.Security, CommissionModule.Catalog,
                     CommissionModule.Planning, CommissionModule.Budget,
                     CommissionModule.Commission, CommissionModule.Report
                });
            Permissions = AsReadOnly(new[] {
                    CommissionPermission.Create, CommissionPermission.Read,
                    CommissionPermission.Update, CommissionPermission.Delete,
                    CommissionPermission.AccessProtectedData, CommissionPermission.Approve,
                    CommissionPermission.SetUpApprovals, CommissionPermission.Compute,
                    CommissionPermission.Export
                });
        }

        public static IEnumerable<UserCanConsumeResourceRequirement> GenerateRequirements()
        {
            return from m in Modules
                   from p in Permissions
                   select new UserCanConsumeResourceRequirement(p, m);
        }
    }
}
