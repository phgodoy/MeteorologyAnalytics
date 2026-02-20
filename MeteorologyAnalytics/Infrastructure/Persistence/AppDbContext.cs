using MeteorologyAnalytics.Domain;
using Microsoft.EntityFrameworkCore;

namespace MeteorologyAnalytics.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Weather> Weather => Set<Weather>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Weather>(entity =>
        {
            entity.ToTable("weather");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Date)
                .HasColumnName("date");

            entity.Property(x => x.TemperatureC)
                .HasColumnName("temperature_c");

            entity.Property(x => x.TemperatureF)
                .HasColumnName("temperature_f");

            entity.Property(x => x.Summary)
                .HasColumnName("summary");
        });
    }
}