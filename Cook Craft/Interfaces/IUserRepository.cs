using Cook_Craft.Models;

namespace Cook_Craft.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<AppUser>> GetAllUsers();
    Task<AppUser> GeUserById(string id);
    string GetUserIdFromContext();
    bool Add(AppUser user);
    bool Update(AppUser user);
    bool Delete(int id);
    bool Save();
}