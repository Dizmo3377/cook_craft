using Microsoft.AspNetCore.Identity;

namespace Cook_Craft.Models;

public class AppUser : IdentityUser
{
    public ICollection<Recipe> Recipes { get; set; }
}