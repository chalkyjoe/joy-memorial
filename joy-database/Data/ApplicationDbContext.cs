using joy_database.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace joy_database.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<Story> Stories { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .Entity<Story>()
            .HasKey(m => m.Id);
        builder.Entity<Story>()
            .HasMany(m => m.Media);
        base.OnModelCreating(builder);
    }
}