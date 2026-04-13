using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using mktsystem.domain.Entities;

namespace mktsystem.infrastructure.Persistence;

public class MktSystemDbContext : IdentityDbContext<Users>
{
    public MktSystemDbContext(DbContextOptions<MktSystemDbContext> options) : base(options)
    {

    }
    
    public DbSet<Students> Students  { get; set; }
    public DbSet<Payments> Payments  { get; set; }
    public DbSet<Classes> Classes { get; set; }
    public DbSet<Fee> Fees { get; set; }

    public DbSet<Attendance> Attendances { get; set; }

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

        modelBuilder.Entity<Attendance>()
            .HasOne(a => a.Teacher)
            .WithMany()
            .HasForeignKey(a => a.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Attendance>()
            .HasOne(a => a.MarkedByUser)
            .WithMany()
            .HasForeignKey(a => a.MarkedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }

}

