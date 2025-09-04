using SLScooter.BusinessLogic;
using SLScooter.DataAccess;
using SLScooter.DataAccess.DataAccessObjects;
using SLScooter.Domain.Entity;

namespace SLScooter.IntegrationTests {
    public class ScooterTests
    {
        private ScooterDbContext _db;
        private ScooterDao _scooterDao;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            DatabaseBl.RebuildDatabase();
        }

        [SetUp]
        public void Setup()
        {
            _db = new ScooterDbContext();
            _scooterDao = new ScooterDao(_db);

            var user = new User("Danny D", 12345678);
            var user2 = new User("Henry H", 87654321);
            _db.Users.Add(user);
            _db.Users.Add(user2);

            var scooter = new Scooter { Brand = "Voi", BatteryCapacity = 100, Status = ScooterStatus.Available };
            var scooter2 = new Scooter { Brand = "Bolt", BatteryCapacity = 40, Status = ScooterStatus.Available };
            var scooter3 = new Scooter { Brand = "Bolt", BatteryCapacity = 10, Status = ScooterStatus.Available };
            _db.Scooters.Add(scooter);
            _db.Scooters.Add(scooter3);

            _db.Trips.AddRange(
                new Trip
                {
                    User = user, Scooter = scooter, StartTime = DateTime.Now.ToUniversalTime(), Distance = 5, Cost = 30
                },
                new Trip
                {
                    User = user, Scooter = scooter, StartTime = DateTime.Now.ToUniversalTime(), Distance = 10, Cost = 50
                },
                new Trip
                {
                    User = user2, Scooter = scooter2, StartTime = DateTime.Now.ToUniversalTime(), Distance = 15, Cost = 70
                }
            );

            _db.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            DatabaseBl.ClearDatabase();
            _db.Dispose();
        }

        [Test]
        public void GetListOfTrips_UserWithTheMostTrips_ReturnsListOfTrips()
        {
            // Arrange
            var user = _db.Users.First(u => u.Name == "Danny D");

            // Act
            var result = _scooterDao.GetAllTripsForUser(user);

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result.All(t => t.UserId == user.Id));
            Assert.That(result.Select(t => t.Distance), Is.EquivalentTo((int[])[5, 10]));
        }

        [Test]
        public void GetAvailableScootersWithMinimumBatteryPercentage_ValidPercent_ReturnsListOfScooters()
        {
            // Arrange & Act
            var scooters = _scooterDao.GetAvailableScootersWithMinimumBatteryPercentage(20);

            // Assert
            Assert.That(scooters, Has.Count.EqualTo(2));
            Assert.That(scooters.All(s => s.BatteryCapacity > 20));
        }

        [Test]
        public void CalculateAverageKmPriceForAllTrips_AllUsers_ReturnsAverageKmPrice()
        {
            // Arrange & Act
            var averageKmPrice = _scooterDao.CalculateAverageKmPriceForAllTrips(null);

            // Assert
            Assert.That(averageKmPrice, Is.EqualTo(5));
        }

        [Test]
        public void GetAllTripsForUser_ValidUser_ReturnsTripsOfUser()
        {
            // Arrange
            var user = _db.Users.First(u => u.Name == "Danny D");

            // Act
            var trips = _scooterDao.GetAllTripsForUser(user);

            // Assert
            Assert.That(trips, Has.Count.EqualTo(2));
            Assert.That(trips.All(t => t.UserId == user.Id));
            Assert.That(trips.Select(t => t.Distance), Is.EquivalentTo((int[])[5, 10]));
        }
    }
}
