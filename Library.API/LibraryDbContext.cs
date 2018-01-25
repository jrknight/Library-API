using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.API
{
    public class LibraryDbContext : DbContext
    {
        
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookGenre>()
                .HasKey(bg => new { bg.BookId, bg.GenreId });

            modelBuilder.Entity<BookGenre>()
                .HasOne(bg => bg.Book)
                .WithMany(b => b.BookGenres)
                .HasForeignKey(bg => bg.BookId);

            modelBuilder.Entity<BookGenre>()
                .HasOne(bg => bg.Genre)
                .WithMany(b => b.BookGenres)
                .HasForeignKey(bg => bg.GenreId);

        }

        // public DbSet<School> Schools { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookGenre> BookGenre { get; set; }
        public DbContextOptions Options { get; }
        public DbSet<BookRequest> BookRequests { get; set; }
        public DbSet<BookRecord> BookRecords { get; set; }
    }
}
