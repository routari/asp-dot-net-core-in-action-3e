using RecipeApplication.Data;
using RecipeApplication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Services

builder.Services.AddRazorPages();

var connString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connString!));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
    options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<RecipeService>();

#endregion

var app = builder.Build();

#region Middleware

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

#endregion

#region Mapping

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();
app.MapControllers();

#endregion

app.Run();
