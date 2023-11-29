using joy_database.Data;
using joy_database.Models;
using joy_database.Repositories;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using joy_memorial.Areas.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services
    .AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddScoped<StoryRepository>();
builder.Services.AddScoped<MediaRepository>();
builder.Services.AddScoped<CategoryRepository>();

var app = builder.Build();
using var scope = app.Services.CreateScope();
var appDb =scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
await appDb.Database.MigrateAsync();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();


async Task<string> CreateRole(string roleName)
{
    IdentityResult roleResult;

// Check if the role exists, create it if not
    var roleExist = await roleManager.RoleExistsAsync(roleName);
    if (!roleExist)
    {
        // Create the role
        roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
    }

    return roleName;
}

var adminRoleName = await CreateRole("Admin");
var privilegedRoleName = await CreateRole("Privileged");

// Create an admin user
IdentityUser adminUser = await userManager.FindByEmailAsync("admin@example.com");

if (adminUser == null)
{
    adminUser = new IdentityUser()
    {
        UserName = "admin@example.com",
        Email = "admin@example.com",
        EmailConfirmed = true
    };
    await userManager.CreateAsync(adminUser, "Admin@123");
}

IdentityUser privilegedUser = await userManager.FindByEmailAsync("user@example.com");

if (privilegedUser == null)
{
    privilegedUser = new IdentityUser
    {
        UserName = "user@example.com",
        Email = "user@example.com",
        EmailConfirmed = true
    };
    await userManager.CreateAsync(privilegedUser, "User@123");
}

// Assign the admin user to the Admin role
await userManager.AddToRoleAsync(adminUser, adminRoleName);
await userManager.AddToRoleAsync(privilegedUser, privilegedRoleName);
app.Run();