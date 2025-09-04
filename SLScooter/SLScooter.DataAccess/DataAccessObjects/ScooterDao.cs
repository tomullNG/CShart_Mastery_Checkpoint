using Microsoft.EntityFrameworkCore;
using SLScooter.DataAccess.Repositories;
using SLScooter.Domain.Entity;

namespace SLScooter.DataAccess.DataAccessObjects;

public class ScooterDao : IScooterRepository
{
    private readonly ScooterDbContext _db;

    public ScooterDao(ScooterDbContext db) {
        _db = db;
    }

    public ICollection<Scooter>? GetAvailableScootersWithMinimumBatteryPercentage(int percentage)
    {
        ICollection<Scooter>? availableScooters =
            _db.Scooters.Select(s => s).Where(s => s.Status == ScooterStatus.Available && s.BatteryCapacity > 20).ToList();
        
        return availableScooters;
    }

    public ICollection<Trip> GetAllTripsForUser(User user) {
        return _db.Trips
            .Where(t => t.UserId == user.Id)
            .ToList();
    }

    public decimal? CalculateAverageKmPriceForAllTrips(User? user) {
        var trips = _db.Trips.AsQueryable();

        if (user is not null) {
            trips = trips.Where(t => t.UserId == user.Id);
        }
        
        if (!trips.Any()) {
            return null;
        }

        decimal totalCost = trips.Sum(t => t.Cost);
        int totalDistance = trips.Sum(t => t.Distance);

        return totalDistance == 0 ? null : totalCost / totalDistance;
    }


    public User? GetUserWithMostTrips()
    {
        User? user = _db.Users
            .OrderByDescending(u => u.Trips!.Count())
            .ThenBy(u => u.Id)
            .FirstOrDefault();

        return user;
    }
}