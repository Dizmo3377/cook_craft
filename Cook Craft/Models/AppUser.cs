using Microsoft.AspNetCore.Identity;

namespace Cook_Craft.Models;

public class AppUser : IdentityUser
{
    public string Id { get; set; }
    public ICollection<Recipe> Recipes { get; set; }
}