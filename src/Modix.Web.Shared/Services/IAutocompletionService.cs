using Modix.Web.Shared.Models.Common;

namespace Modix.Web.Shared.Services;

public interface IAutocompletionService
{
    Task<IEnumerable<ModixUser>?> AutocompleteUsersAsync(string query);
}
