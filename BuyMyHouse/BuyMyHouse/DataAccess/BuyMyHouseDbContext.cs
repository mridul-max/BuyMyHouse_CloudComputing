using BuyMyHouse.Model;
using Microsoft.EntityFrameworkCore;

namespace BuyMyHouse.DataAccess
{
    public class BuyMyHouseDbContext : DbContext, IBuyMyHouseDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Mortgage> Mortgages { get; set; }
        public DbSet<House> Houses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseCosmos(
                        Environment.GetEnvironmentVariable("DBURI"),
                        Environment.GetEnvironmentVariable("dbkey"),
                        Environment.GetEnvironmentVariable("DBName"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToContainer("Users").HasNoDiscriminator()
                .HasPartitionKey(u=> u.ZipCode)
                .HasKey(u => u.Id);
            modelBuilder.Entity<Mortgage>().ToContainer("Mortgages").HasNoDiscriminator()
                .HasPartitionKey(m => m.ZipCode).HasKey(m => m.Id);
            modelBuilder.Entity<Mortgage>().ToContainer("Houses").HasNoDiscriminator()
                .HasPartitionKey(h => h.ZipCode).HasKey(h => h.Id);

        }

       /* public void MarkAsModifiedCareGiver(CareGiver careGiver)
        {
            throw new NotImplementedException();
        }

        public void MarkAsModifiedDrinkRecord(DrinkRecord drinkRecord)
        {
            throw new NotImplementedException();
        }

        public void MarkAsModifiedPatient(Patient patient)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }*/

    }
}
