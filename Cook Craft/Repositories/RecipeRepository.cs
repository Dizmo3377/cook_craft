using Cook_Craft.Data;
using Cook_Craft.Interfaces;
using Cook_Craft.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Cook_Craft.Repositories;

public class RecipeRepository : IRecipeRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IUserRepository _userManager;

    public RecipeRepository(ApplicationDbContext context, IUserRepository userRepository)
    {
        _context = context;
        _userManager = userRepository;
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
        var recipe = await _context.Recipes
            .Include(r => r.Steps)
            .Include(r => r.Ingridients)
            .FirstOrDefaultAsync(r => r.Id == id);

        return recipe;
    }
}