using SLScooter.Domain.Entity;

namespace SLScooter.DataAccess.Repositories;

public interface IScooterRepository
{
    ICollection<Scooter>? GetAvailableScootersWithMinimumBatteryPercentage(int percentage);
    ICollection<Trip>? GetAllTripsForUser(User user);
    decimal? CalculateAverageKmPriceForAllTrips(User? user);
    User? GetUserWithMostTrips();
}