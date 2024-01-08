using System.ComponentModel.DataAnnotations.Schema;

namespace Cook_Craft.Models;

public class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public double? PrepTime{ get; set;}
    public double? CookTime{ get; set;}
    public string Image { get; set; }
    public int Rating { get; set;}
    public ICollection<Step> Steps { get; set; }
    public ICollection<Ingridient> Ingridients { get; set; }
}