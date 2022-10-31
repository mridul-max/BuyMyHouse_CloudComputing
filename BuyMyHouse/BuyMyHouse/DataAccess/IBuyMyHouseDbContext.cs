using BuyMyHouse.Model;
using Microsoft.EntityFrameworkCore;

namespace BuyMyHouse.DataAccess
{
    public interface IBuyMyHouseDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Mortgage> Mortgages { get; set; }
        public DbSet<House> Houses { get; set; }
        Task<int> SaveChangesAsync();
/*        public void MarkAsModifiedPatient(Patient patient);
        public void MarkAsModifiedCareGiver(CareGiver careGiver);
        public void MarkAsModifiedDrinkRecord(DrinkRecord drinkRecord);*/
    }
}
