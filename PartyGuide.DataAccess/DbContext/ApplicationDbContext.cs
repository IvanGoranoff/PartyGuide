using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PartyGuide.DataAccess.Data;
using System.Reflection.Metadata;

namespace PartyGuide.DataAccess.DbContext
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ServiceTable> ServiceTables { get; set; }

        public DbSet<RatingTable> RatingTables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RatingTable>()
                .HasOne(e => e.Service)
                .WithMany(e => e.Ratings)
                .HasForeignKey(e => e.ServiceId);
        }
    }
}