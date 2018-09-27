using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSounds.Backend.Data
{
  public class SeedDatabase
  {
    private static readonly string[] Roles = new string[] { "SystemAdmin", "Admin", "User"};

    private readonly SystemSettings _systemSettings;

    public SeedDatabase(IOptions<SystemSettings> systemSettings)
    {
      _systemSettings = systemSettings.Value;
    }

    public static async Task SeedRoles(IServiceProvider serviceProvider)
    {
      using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
      {
        var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

        if (!dbContext.Roles.Any())
        {
          var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

          foreach (var role in Roles)
          {
            if (!await roleManager.RoleExistsAsync(role))
            {
              await roleManager.CreateAsync(new IdentityRole(role));
            }
          }
        }
      }
    }

    public static async Task SeedSystemAdminUser(IServiceProvider serviceProvider, string adminUser, string adminPassword)
    {
      using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
      {
        var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
        var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        
        var user = await userManager.FindByNameAsync(adminUser);
        if (user == null)
        {
          var userASP = new IdentityUser
          {
            Email = adminUser,
            UserName = adminUser
          };

          var result =  userManager.CreateAsync(userASP, adminPassword);
          if (result.Result.Succeeded)
          {
            await userManager.AddToRoleAsync(userASP, "SystemAdmin");
          }
        }
      }
    }
  }
}
