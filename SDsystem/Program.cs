using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SDsystem.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<UserModel, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login"; // Redirects to the correct login page
    options.AccessDeniedPath = "/Identity/Account/AccessDenied"; // Redirects unauthorized users
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", context =>
{
    context.Response.Redirect("/Identity/Account/Login");
    return Task.CompletedTask;
});

app.MapRazorPages();
app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserModel>>();

    if (!await roleManager.RoleExistsAsync("Coordinator"))
    {
        await roleManager.CreateAsync(new IdentityRole("Coordinator"));
    }

    if (!await roleManager.RoleExistsAsync("User"))
    {
        await roleManager.CreateAsync(new IdentityRole("User"));
    }

    var adminUser = await userManager.FindByEmailAsync("admin@mail.com");
    if (adminUser != null && !await userManager.IsInRoleAsync(adminUser, "Coordinator"))
    {
        await userManager.AddToRoleAsync(adminUser, "Coordinator");
    }

    var normalUser = await userManager.FindByEmailAsync("user@mail.com");
    if (normalUser != null && !await userManager.IsInRoleAsync(normalUser, "User"))
    {
        await userManager.AddToRoleAsync(normalUser, "User");
    }
}
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
