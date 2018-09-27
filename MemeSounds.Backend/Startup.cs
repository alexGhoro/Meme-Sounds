using MemeSounds.Backend.Data;
using MemeSounds.Backend.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MemeSounds.Backend
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.Configure<CookiePolicyOptions>(options =>
      {
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });

      var DbConnectionString = BuildDBConnectionString();
      services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(DbConnectionString));

      services.Configure<SystemSettings>(Configuration.GetSection("SystemSettings"));

      services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultUI();

      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

      services.AddOptions();

    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseDatabaseErrorPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseCookiePolicy();

      app.UseAuthentication();

      SeedDataBase(app);

      app.UseMvc(routes =>
      {
        routes.MapRoute(
          name: "default",
          template: "{controller=Home}/{action=Index}/{id?}");
      });
    }

    private void SeedDataBase(IApplicationBuilder app)
    {
      SeedDatabase.SeedRoles(app.ApplicationServices).Wait();
      SeedDatabase.SeedSystemAdminUser(app.ApplicationServices,
        Configuration["AdminUser"],
        Configuration["AdminPassword"])
        .Wait();
    }

    private string BuildDBConnectionString()
    {
      var builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("AzureConnection"))
      {
        Password = Configuration["DbPassword"]
      };
      return builder.ConnectionString;
    }
  }
}
