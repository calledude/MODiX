using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Modix.Data.Models.Core;
using Modix.Data.Utilities;

namespace Modix.Models.Core;

public class ClaimInfoData
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required AuthorizationClaimCategory Category { get; init; }

    private static Dictionary<AuthorizationClaim, ClaimInfoData>? _cachedClaimData;

    public static Dictionary<AuthorizationClaim, ClaimInfoData> GetClaims()
    {
        _cachedClaimData ??= typeof(AuthorizationClaim).GetFields(BindingFlags.Public | BindingFlags.Static).ToDictionary
        (
            d => (AuthorizationClaim)d.GetValue(null)!,
            d =>
            {
                var claimInfo = (ClaimInfoAttribute)d.GetCustomAttributes(typeof(ClaimInfoAttribute), true).First()!;

                return new ClaimInfoData
                {
                    Name = d.Name,
                    Description = claimInfo.Description,
                    Category = claimInfo.Category
                };
            }
        );

        return _cachedClaimData;
    }
}
