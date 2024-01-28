using Cook_Craft.Models;
using System.Collections;

namespace Cook_Craft.Interfaces;

public interface IKitchenRepository
{
    public ICollection<Recipe> GetAllRecipes();
}