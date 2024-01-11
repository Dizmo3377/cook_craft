using Cook_Craft.Data;
using Cook_Craft.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Cook_Craft.Repositories;

public class KitchenRepository : IKitchenRepository
{
    private readonly ApplicationDbContext _context;

    public KitchenRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public ICollection GetAllRecipes()
    {
        return _context.Recipes.Include(r => r.AppUser).ToList();
    }
}