using LibraryManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Infrastructure.Persistence;

public class LibraryManagerDbContext(DbContextOptions<LibraryManagerDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Book>(e =>
        {
            e.ToTable("Books");
            e.HasKey(b => b.Id);
            e.HasOne(b => b.CurrentLoan)
                .WithOne()
                .HasForeignKey<Book>(b => b.CurrentLoanId)
                .IsRequired(false) // Nullable
                .OnDelete(DeleteBehavior.SetNull); // If Loan is deleted, Book.CurrentLoanId is set to null
        });
        builder.Entity<User>(e =>
        {
            e.ToTable("Users");
            e.HasKey(u => u.Id);
        });
        builder.Entity<Loan>(e =>
        {
            e.ToTable("Loans");
            e.HasKey(l => l.Id);
            e.HasOne(l => l.User)
                .WithMany(u=>u.Loans)
                .HasForeignKey(l=>l.IdUser)
                .OnDelete(DeleteBehavior.Restrict);
            e.HasOne(l => l.Book)
                .WithMany(b => b.Loans)
                .HasForeignKey(l => l.IdBook)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        base.OnModelCreating(builder);
    }
}