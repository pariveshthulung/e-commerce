using ebay.Entity;

namespace ebay.Provider.Interface;

public interface ICurrentUserProvider
{
    bool IsLoggedIn();
    Task<User?> GetCurrentUser();
    int? GetCurrentUserId();
}
