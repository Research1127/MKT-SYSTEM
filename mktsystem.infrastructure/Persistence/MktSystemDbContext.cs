using Microsoft.EntityFrameworkCore;
using mktsystem.domain.Entities;

namespace mktsystem.infrastructure.Persistence;

public class MktSystemDbContext : DbContext
{
    public MktSystemDbContext(DbContextOptions<MktSystemDbContext> options) : base(options)
    {

    }
    
    public DbSet<Students> Students  { get; set; }
    public DbSet<Payments> Payments  { get; set; }
    public DbSet<Classes> Classes { get; set; }
    public DbSet<Fee> Fees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Payments>()
            .HasOne(c => c.Student)
            .WithMany(p  => p.Payments)
            .HasForeignKey(k => k.StudentId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Students>()
            .HasOne(c => c.Class)
            .WithMany(p => p.Students)
            .HasForeignKey(k => k.ClassId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}

