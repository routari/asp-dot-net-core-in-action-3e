using Microsoft.EntityFrameworkCore;

namespace RecipeApplication.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Recipe> Recipes { get; set; } = null!;
}
