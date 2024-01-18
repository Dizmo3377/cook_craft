using Cook_Craft.Interfaces;
using Cook_Craft.Models;
using Cook_Craft.Services;
using Cook_Craft.View_Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web;
using Cook_Craft.Extensions;
using AutoMapper;
using Cook_Craft.Data;

namespace Cook_Craft.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public RecipeController(IRecipeRepository recipeRepository,
                                IPhotoService photoService,
                                IMapper mapper,
                                IUserRepository userRepository)
        {
            _recipeRepository = recipeRepository;
            _photoService = photoService;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        //Get
        public IActionResult Index()
        {
            var userIdFromContext = _userRepository.GetUserIdFromContext();
            var recipeViewModel = new CreateRecipeViewModel { AppUserId = userIdFromContext};
            return View(recipeViewModel);
        }

        //Post
        [HttpPost]
        public async Task<IActionResult> Index(CreateRecipeViewModel recipeVM)
        {
            if (!ModelState.IsValid) ModelState.AddModelError("", "Photo upload failed");

            var result = await _photoService.AddPhotoAsync(recipeVM.Image);

            var recipe = _mapper.Map<Recipe>(recipeVM);
            recipe.Image = result.Url.ToString();

            _recipeRepository.Create(recipe);
            return RedirectToAction("Index", "Kitchen");
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

            var recipeViewModel = _mapper.Map<EditRecipeViewModel>(recipe);

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
                await _photoService.DeletePhotoAsync(recipeVM.URL);
                var photo = await _photoService.AddPhotoAsync(recipeVM.Image);
                photoResult = photo.Url.ToString();
                recipeVM.URL = photoResult;
            }
            else
            {
                photoResult = recipeVM.URL;
            }

            var recipe = _mapper.Map<Recipe>(recipeVM);
            recipe.Image = photoResult;

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
