var builder = WebApplication.CreateBuilder(args);

#region Services

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.AppendTrailingSlash = true;
    options.LowercaseQueryStrings = true;
});

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

app.UseStatusCodePagesWithReExecute("/Error/{0}");

app.UseRouting();

app.UseAuthorization();

#endregion

#region Mapping

// .NET 9+ static asset handling:
// The key change is that static files are now represented as endpoints,
// instead of middleware intercepting and shortcircuiting requests.
// ASP.NET Core generates endpoint metadata during startup, eg:
// Endpoint: /css/site.css
// At build time ASP.NET Core now already knows which files exists.
// No searching the filesystem each time. It's precomputed.
// A further push towards AOT compilation.

// Create static asset endpoint mappings
app.MapStaticAssets(); // Now an endpoint, not a middleware

app.MapRazorPages()
    .WithStaticAssets(); // Tell Razor Pages to integrate with the new static asset system.

#endregion

app.Run();
