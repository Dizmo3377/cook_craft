using Cook_Craft.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cook_Craft.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Ingridient> Ingridients { get; set; }
    }
}