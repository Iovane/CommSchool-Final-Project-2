using CommSchool_Final_Project_2.Data;
using Microsoft.EntityFrameworkCore;

namespace CommSchool_Final_Project_2.Domain;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Loan>()
            .Property(l => l.LoanType)
            .HasConversion<string>();

        modelBuilder.Entity<Loan>()
            .Property(l => l.Status)
            .HasConversion<string>();

        modelBuilder.Entity<Loan>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RefreshToken>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(r => r.UserId);
    }
}