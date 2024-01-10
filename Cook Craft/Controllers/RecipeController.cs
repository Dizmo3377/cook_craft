using Cook_Craft.Interfaces;
using Cook_Craft.Models;
using Cook_Craft.Services;
using Cook_Craft.View_Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Cook_Craft.Extensions;

namespace Cook_Craft.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContext;

        public RecipeController(IRecipeRepository recipeRepository,
                                IPhotoService photoService,
                                IHttpContextAccessor httpContext)
        {
            _recipeRepository = recipeRepository;
            _photoService = photoService;
            _httpContext = httpContext;
        }

        //Get
        public IActionResult Index()
        {
            var userIdFromContext = _httpContext.HttpContext?.User.GetUserId();
            var recipeViewModel = new CreateRecipeViewModel { AppUserId = userIdFromContext};
            return View(recipeViewModel);
        }

        //Post
        [HttpPost]
        public async Task<IActionResult> Index(CreateRecipeViewModel recipeVM)
        {
            if (!ModelState.IsValid) ModelState.AddModelError("", "Photo upload failed");

            var result = await _photoService.AddPhotoAsync(recipeVM.Image);

            var steps = recipeVM.Steps
                .Select(step => new Step { Description = step })
                .ToList();

            var ingridients = recipeVM.Ingridients
                .Select(ingredient => new Ingridient { Name = ingredient })
                .ToList();

            var recipe = new Recipe
            {
                Name = recipeVM.Name,
                Description = recipeVM.Description,
                PrepTime = recipeVM.PrepTime,
                CookTime = recipeVM.CookTime,
                Image = result.Url.ToString(),
                Rating = recipeVM.Rating,
                Steps = steps,
                Ingridients = ingridients,
                AppUserId = recipeVM.AppUserId
            };

            _recipeRepository.Create(recipe);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Show(int id)
        {
            var recipe = await _recipeRepository.GetRecipeAsync(id);

            if (recipe == null) return NotFound();

            return View(recipe);
        }
    }
}
