﻿using Cook_Craft.Models;
using System.ComponentModel.DataAnnotations;

namespace Cook_Craft.View_Models;

public class CreateRecipeViewModel
{
    [Required] public string Name { get; set; }
    public string? Description { get; set; }
    public double? PrepTime { get; set; }
    public double? CookTime { get; set; }
    [Required] public IFormFile Image { get; set; }
    public int Rating { get; set; }
    public List<string> Steps { get; set; }
    public List<string> Ingridients { get; set; }
    public string? AppUserId { get; set; }
}