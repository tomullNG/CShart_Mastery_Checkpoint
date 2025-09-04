using Microsoft.EntityFrameworkCore;
using SLScooter.Domain.Entity;

namespace SLScooter.DataAccess {
    public class ScooterDbContext : DbContext {
        public DbSet<Scooter> Scooters => Set<Scooter>();
        public DbSet<Trip> Trips => Set<Trip>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            // TODO: Add partial primary keys here
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder
                .UseNpgsql(
                    "Host = localhost:54323; " +
                    "Username = postgres; " +
                    "Password = Password123!; " +
                    "Database = scooters;")
                .UseLowerCaseNamingConvention();
        }
    }
}
