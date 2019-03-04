using System;
using System.Collections.Generic;
using System.Text;

namespace NA.Template.Entities.Authorization
{
    public enum CommissionPermission : byte
    {
        Read = 1,
        Create = 2,
        Update = 3,
        Delete = 4,

        AccessProtectedData = 5,
        Approve = 6,
        SetUpApprovals = 7,
        Compute = 8,
        Export = 9,
    }
}
