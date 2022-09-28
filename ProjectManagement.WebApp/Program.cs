#region Usings
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Data.Context;
using ProjectManagement.IoC;
#endregion


#region Services
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

//DbContext
builder.Services.AddDbContext<PMDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

//Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.ExpireTimeSpan = TimeSpan.FromDays(10);
    });

DependencyContainer.RegisterDependency(builder.Services);
#endregion


#region Configurations
var app = builder.Build();

app.UseHsts();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//Initialize Data
using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService<PMDbContext>();
    // auto migration
    if (context != null)
        context.Database.Migrate();
}

app.Run();
#endregion
