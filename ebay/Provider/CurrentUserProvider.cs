using System.Security.Claims;
using ebay.Data;
using ebay.Entity;
using ebay.Provider.Interface;

namespace ebay.Provider;

public class CurrentUserProvider : ICurrentUserProvider
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _contextAccessor;

    public CurrentUserProvider(ApplicationDbContext context,IHttpContextAccessor contextAccessor)
    {
        _context = context;
        _contextAccessor = contextAccessor;
    }
    public async Task<User?> GetCurrentUser()
    {
        var currentUserId = GetCurrentUserId();
        if(!currentUserId.HasValue) return null;
        return await _context.Users.FindAsync(currentUserId.Value);
    }

    public int? GetCurrentUserId()
    {
        var userId = _contextAccessor.HttpContext?.User.FindFirstValue("Id");
        if(string.IsNullOrEmpty(userId)) return null;
        return Convert.ToInt32(userId);
    }

    public bool IsLoggedIn()
        =>GetCurrentUserId() != null;
}
