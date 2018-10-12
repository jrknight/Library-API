using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace Library.API
{
    public class ReCircleDbContext : IdentityDbContext<User>
    {
        
        public ReCircleDbContext(DbContextOptions<ReCircleDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ItemRequest>()
                .HasOne(br => br.User)
                .WithMany(u => u.ItemRequests)
                .HasForeignKey(br => br.UserId);

            modelBuilder.Entity<ItemRequest>()
                .HasOne(br => br.Item)
                .WithMany(b => b.ItemRequests)
                .HasForeignKey(br => br.ItemId);



            modelBuilder.Entity<ItemRecord>()
                .HasOne(br => br.User)
                .WithMany(u => u.ItemRecords)
                .HasForeignKey(br => br.UserId);

            modelBuilder.Entity<ItemRecord>()
                .HasOne(br => br.Item)
                .WithMany(b => b.ItemRecords)
                .HasForeignKey(br => br.ItemId);
            
        }

        // public DbSet<School> Schools { get; set; }

        public DbContextOptions Options { get; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemRequest> ItemRequests { get; set; }
        public DbSet<ItemRecord> ItemRecords { get; set; }
    }
}
