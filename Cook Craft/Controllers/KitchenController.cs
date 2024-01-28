using Cook_Craft.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cook_Craft.Controllers
{
    public class KitchenController : Controller
    {
        private readonly IKitchenRepository _kitchenRepository;

        public KitchenController(IKitchenRepository kitchenRepository)
        {
            _kitchenRepository = kitchenRepository;
        }

        public IActionResult Index(string sorting)
        {
            var recipes = _kitchenRepository.GetAllRecipes();

            switch (sorting)
            {
                case "asc":
                    recipes = recipes.OrderBy(r => r.CookTime).ToList();
                    break;
                case "desc":
                    recipes = recipes.OrderByDescending(r => r.CookTime).ToList();
                    break;
                default: 
                    break;
            }

            return View(recipes);
        }
    }
}
