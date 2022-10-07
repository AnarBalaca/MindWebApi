using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mind.Entity.Entities;
using Mind.Entity.Identity;

namespace Mind.Data.DAL;

public class AppDbContext : IdentityDbContext<AppUser>
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public AppDbContext(DbContextOptions option) : base(option) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public DbSet<Psychologist> Psychologists  { get; set; }
    public DbSet<Image> Images { get; set; }

}
