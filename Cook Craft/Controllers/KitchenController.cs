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

        public IActionResult Index()
        {
            var recipes = _kitchenRepository.GetAllRecipes();
            return View(recipes);
        }
    }
}
