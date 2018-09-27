using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MemeSounds.Domain;
using Microsoft.AspNetCore.Identity;

namespace MemeSounds.Backend.Data
{

  public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<User> User { get; set; }
    public DbSet<MemeSounds.Domain.UserType> UserType { get; set; }
  }
}


