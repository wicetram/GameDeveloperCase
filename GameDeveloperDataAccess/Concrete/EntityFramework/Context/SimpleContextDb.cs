using GameDeveloperEntity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace GameDeveloperDataAccess.Concrete.EntityFramework.Context
{
    public class SimpleContextDb : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=TestDb;Integrated Security=True");
        }

        public DbSet<Admin>? Admin { get; set; }
        public DbSet<User>? User { get; set; }
        public DbSet<Queque>? Queque { get; set; }
    }
}
