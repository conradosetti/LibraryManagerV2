using LibraryManager.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.API.Persistence;

public class LibraryManagerDbContext(DbContextOptions<LibraryManagerDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Book>(e =>
        {
            e.HasKey(b => b.Id);
        });
        builder.Entity<User>(e =>
        {
            e.HasKey(u => u.Id);
        });
        builder.Entity<Loan>(e =>
        {
            e.HasKey(l => l.Id);
            e.HasOne(l => l.User)
                .WithMany(u=>u.Loans)
                .HasForeignKey(l=>l.IdUser)
                .OnDelete(DeleteBehavior.Restrict);
            e.HasOne(l => l.Book)
                .WithOne(b => b.Loan)
                .HasForeignKey<Loan>(l => l.IdBook);
        });
        
        base.OnModelCreating(builder);
    }
}