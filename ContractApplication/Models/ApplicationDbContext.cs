using Microsoft.EntityFrameworkCore;

namespace ContractApplication.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Contractor> Contractors { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contract>()
                .HasKey(x => new { x.Contractor1Id, x.Contractor2Id });

            modelBuilder.Entity<Contract>()
                .HasOne(c => c.Contractor1)
                .WithMany(c => c.ContractFrom)
                .HasForeignKey(c => c.Contractor1Id);

            modelBuilder.Entity<Contract>()
                .HasOne(c => c.Contractor2)
                .WithMany(c => c.ContractTo)
                .HasForeignKey(c => c.Contractor2Id);
        }
    }
}
