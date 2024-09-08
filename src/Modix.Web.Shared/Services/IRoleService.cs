using Modix.Web.Shared.Models.Common;

namespace Modix.Web.Shared.Services;

public  interface IRoleService
{
    Task<Dictionary<ulong, RoleInformation>> GetRolesAsync();
}
