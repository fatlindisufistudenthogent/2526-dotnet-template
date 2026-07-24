using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rise.Domain.Products;
using Rise.Domain.Projects;

namespace Rise.Persistence;

/// <summary>
/// Entrance to the database, inherits from IdentityDbContext and is basically a Unit Of Work and Repository pattern combined.
/// A <see cref="DbSet"/> is a repository for a specific type of entity.
/// The <see cref="ApplicationDbContext"/> is the Unit Of Work pattern
/// Will look very similar when switching database providers.
/// See https://hogent-web.github.io/csharp/chapters/09/slides/index.html#1
/// See https://enterprisecraftsmanship.com/posts/should-you-abstract-database/
/// </summary>
/// <param name="opts"></param>
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts) : IdentityDbContext<IdentityUser>(opts)
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Technician> Technicians => Set<Technician>();

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        // All columns in the database have a maxlength of 4000.
        // in NVARACHAR 4000 is the maximum length that can be indexed by a database.
        // Some columns need more length, but these can be set on the configuration level for that Entity in particular.
        configurationBuilder.Properties<string>().HaveMaxLength(4_000);
        // All decimals columns should have 2 digits after the comma
        configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Applying all types of IEntityTypeConfiguration in the Persistence project.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        // fix for MariaDB max length of IdentityUser and IdentityRole
        modelBuilder.Entity<IdentityRole>(entity =>
        {
            entity.Property(e => e.Id).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.NormalizedName).HasMaxLength(255);
        });

        modelBuilder.Entity<IdentityUser>(entity =>
        {
            entity.Property(e => e.Id).HasMaxLength(255);
            entity.Property(e => e.PasswordHash).HasColumnType("longtext");
            entity.Property(e => e.SecurityStamp).HasColumnType("longtext");
            entity.Property(e => e.ConcurrencyStamp).HasColumnType("longtext");
            entity.Property(e => e.PhoneNumber).HasColumnType("longtext");
        });

        modelBuilder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.Property(e => e.UserId).HasMaxLength(255);
            entity.Property(e => e.RoleId).HasMaxLength(255);
        });

        modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
        {
            entity.Property(e => e.UserId).HasMaxLength(255);
        });

        modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.Property(e => e.UserId).HasMaxLength(255);
            entity.Property(e => e.LoginProvider).HasMaxLength(255);
            entity.Property(e => e.ProviderKey).HasMaxLength(255);
        });

        modelBuilder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.Property(e => e.UserId).HasMaxLength(255);
            entity.Property(e => e.LoginProvider).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
        {
            entity.Property(e => e.RoleId).HasMaxLength(255);
        });

    }
}
