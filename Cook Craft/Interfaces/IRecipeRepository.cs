using Cook_Craft.Models;

namespace Cook_Craft.Interfaces;

public interface IRecipeRepository
{
    bool Create(Recipe recipe);
    bool Update(Recipe recipe);
    bool Delete(Recipe recipe);
    bool Save();
}