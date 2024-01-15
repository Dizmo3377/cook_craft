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
                AppUserId = recipeVM.AppUserId,
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var recipe = await _recipeRepository.GetRecipeAsync(id);

            if (recipe == null) return NotFound();

            var steps = recipe.Steps
                .Select(step => step.Description)
                .ToList();

            var ingridients = recipe.Ingridients
                .Select(ingredient => ingredient.Name)
                .ToList();

            var recipeViewModel = new EditRecipeViewModel
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description,
                PrepTime = recipe.PrepTime,
                CookTime = recipe.CookTime,
                URL = recipe.Image,
                Rating = recipe.Rating,
                Steps = steps,
                Ingridients = ingridients,
                AppUserId = recipe.AppUserId
            };

            return View(recipeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRecipeViewModel recipeVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Editing went wrong");
                return View("Edit", recipeVM);
            }

            string photoResult;

            if (recipeVM.Image != null)
            {
                var photo = await _photoService.AddPhotoAsync(recipeVM.Image);
                photoResult = photo.Url.ToString();
                recipeVM.URL = photoResult;
            }
            else
            {
                photoResult = recipeVM.URL;
            }

            var steps = recipeVM.Steps
                .Select(step => new Step { Description = step })
                .ToList();

            var ingridients = recipeVM.Ingridients
                .Select(ingredient => new Ingridient { Name = ingredient })
                .ToList();

            var recipe = new Recipe
            {
                Id = recipeVM.Id,
                Name = recipeVM.Name,
                Description = recipeVM.Description,
                PrepTime = recipeVM.PrepTime,
                CookTime = recipeVM.CookTime,
                Image = photoResult,
                Rating = recipeVM.Rating,
                Steps = steps,
                Ingridients = ingridients,
                AppUserId = recipeVM.AppUserId
            };

            _recipeRepository.Update(recipe);

            return RedirectToAction("Index", "Kitchen");
        }


        public async Task<IActionResult> Delete(int id)
        {
            var recipe = await _recipeRepository.GetRecipeAsync(id);

            if (recipe == null) return NotFound();

            await _photoService.DeletePhotoAsync(recipe.Image);
            _recipeRepository.Delete(recipe);

            return RedirectToAction("Index", "Kitchen");
        }
    }
}
