using Microsoft.EntityFrameworkCore;
using ST10070933PROG6212POE2.Models;

namespace ST10070933PROG6212POE2.Data
{
  
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }

            public DbSet<Claim> Claims { get; set; }
            public DbSet<SupportingDocument> SupportingDocuments { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<Claim>()
                    .HasMany(c => c.Documents)
                    .WithOne(d => d.Claim)
                    .HasForeignKey(d => d.ClaimId)
                    .OnDelete(DeleteBehavior.Cascade);
            }
        }
    }

