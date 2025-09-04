using SLScooter.DataAccess.Repositories;
using SLScooter.Domain.Entity;

namespace SLScooter.BusinessLogic {
    public class ScooterBl(IScooterRepository repo) {
        public void ListAvailableScootersWithMinimumBatteryPercentage(int percentage)
        {
            ICollection<Scooter>? scooters = repo.GetAvailableScootersWithMinimumBatteryPercentage(percentage);

            if (scooters == null || scooters.Count == 0)
            {
                Console.WriteLine($"No scooters with battery higher than {percentage}% were found.");
                return;
            }

            foreach (var scooter in scooters)
            {
                Console.WriteLine(
                    $"Scooter {scooter.Id} with battery {scooter.BatteryCapacity}"); // TODO: Change to Battery (not capacity)
            }
        }

        public void GetAllTripsForUser(User user)
        {
            var trips = repo.GetAllTripsForUser(user);

            if (trips == null || trips.Count == 0)
            {
                Console.WriteLine($"User has no trips.");
                return;
            }

            Console.WriteLine($"Trips for user {user.Id}");
            foreach (var trip in trips)
            {
                Console.WriteLine($"Trip: {trip.Id} at date {trip.StartTime} with scooter {trip.ScooterId}");
            }
        }

        public void CalculateAverageKmPriceForAllTrips(User? user)
        {
            decimal? averageKmPrice = repo.CalculateAverageKmPriceForAllTrips(user ?? null);

            if (!averageKmPrice.HasValue)
            {
                Console.WriteLine($"Could not calculate average price as no trips were found.");
                return;
            }
            Console.WriteLine(user != null 
                ? $"Average price per km for user {user.Id} is {averageKmPrice}" 
                : $"Average price per km for all users is {averageKmPrice}");
        }

        public void GetUserWithMostTrips()
        {
            var user = repo.GetUserWithMostTrips();
            Console.WriteLine(user != null 
                ? $"User with the most trips is: {user.Name} with Id {user.Id}" 
                : $"No user found.");
        }
    }
}
