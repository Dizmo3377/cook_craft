using Cook_Craft.Data;
using Cook_Craft.Interfaces;
using Cook_Craft.Models;
using Microsoft.EntityFrameworkCore;

namespace Cook_Craft.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContext;

    public UserRepository(ApplicationDbContext context, IHttpContextAccessor httpContext)
    {
        _context = context;
        _httpContext = httpContext;
    }

    public bool Add(AppUser user)
    {
        _context.Users.Add(user);
        return Save();
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<AppUser>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<AppUser> GeUserById(string id)
    {
        return await _context.Users.FindAsync(id);
    }

    public string GetUserIdFromContext()
    {
        var userName = _httpContext.HttpContext?.User.Identity.Name;
        var user = _context.Users.FirstOrDefaultAsync(x => x.UserName == userName).Result;
        return user.Id;
    }

    public bool Save()
    {
        int saved = _context.SaveChanges();
        return saved > 0;
    }

    public bool Update(AppUser user)
    {
        _context.Update(user);
        return Save();
    }
}