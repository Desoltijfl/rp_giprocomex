using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using rp_giprocomex.Data;
using rp_giprocomex.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuration / Connection string
var configuration = builder.Configuration;
var conn = configuration.GetConnectionString("DefaultConnection")
    ?? "Server=localhost;Database=rp_giprocomex_db;Trusted_Connection=True;TrustServerCertificate=True;";

// Add services to the container.

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(conn));

// Identity (default UI uses Razor Pages)
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

// Controllers + Razor Pages + Blazor Server
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Register application services
builder.Services.AddScoped<EmployeeService>();

// Optional: helpful in development to show EF migrations exceptions
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // Enables endpoints that help with EF migrations during development (optional)
    // app.UseMigrationsEndPoint(); // If you add package Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Map endpoints in the correct order
app.MapControllers();

// Map Razor Pages so /_Host and Identity UI pages are registered
app.MapRazorPages();

// Blazor Hub for Server
app.MapBlazorHub();

// Fallback to the host page for Blazor Server
app.MapFallbackToPage("/_Host");

// Apply migrations and seed initial data (roles + admin). Log errors if any.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        // Apply pending migrations (optional but convenient during development)
        var db = services.GetRequiredService<AppDbContext>();
        await db.Database.MigrateAsync();

        // Seed roles and admin user
        await DataSeeder.SeedAsync(services);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while migrating or seeding the database.");
        // Rethrow so the startup fails and you can see the error in development
        throw;
    }
}

app.Run();