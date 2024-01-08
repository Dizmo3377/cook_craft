using Cook_Craft.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cook_Craft.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeController(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
