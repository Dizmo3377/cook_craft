using Cook_Craft.Data;
using Cook_Craft.Interfaces;
using Cook_Craft.Models;
using Microsoft.EntityFrameworkCore;

namespace Cook_Craft.Repositories;

public class RecipeRepository : IRecipeRepository
{
    private readonly ApplicationDbContext _context;

    public RecipeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public bool Create(Recipe recipe)
    {
        _context.Recipes.Add(recipe);
        return Save();
    }

    public bool Delete(Recipe recipe)
    {
        _context.Recipes.Remove(recipe);
        return Save();
    }

    public bool Update(Recipe recipe)
    {
        _context.Recipes.Update(recipe);
        return Save();
    }

    public bool Save()
    {
        int saved = _context.SaveChanges();
        return saved > 0;
    }

    public async Task<Recipe> GetRecipeAsync(int id)
    {
        return await _context.Recipes
            .Include(r => r.Steps)
            .Include(r => r.Ingridients)
            .FirstOrDefaultAsync(r => r.Id == id);
    }
}