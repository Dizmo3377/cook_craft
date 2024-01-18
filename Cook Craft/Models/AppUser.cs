using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cook_Craft.Models;

public class AppUser : IdentityUser
{
    public string Id { get; set; }
    public ICollection<Recipe> Recipes { get; set; }
}