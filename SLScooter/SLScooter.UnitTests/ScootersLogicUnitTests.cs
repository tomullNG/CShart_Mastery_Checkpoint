using Moq;
using SLScooter.BusinessLogic;
using SLScooter.DataAccess;
using SLScooter.DataAccess.Repositories;
using SLScooter.Domain.Entity;

namespace SLScooter.UnitTests {
    public class ScootersLogicUnitTests {
        private readonly Mock<IScooterRepository> _mockScooterDa = new(MockBehavior.Strict);

        [SetUp]
        public void Setup() {
        }

        [Test]
        public void Scooter_WithSameId_CannotHaveTwoDifferentStatusesSimultaneously() {
            using var db = new ScooterDbContext();

            var s1 = new Scooter { Id = 1, Brand = "Voi", BatteryCapacity = 100, Status = ScooterStatus.Available };
            db.Scooters.Add(s1);
            db.SaveChanges();

            var s2 = new Scooter { Id = 1, Brand = "Voi", BatteryCapacity = 100, Status = ScooterStatus.OutOfOrder };

            Assert.Throws<InvalidOperationException>(() => db.Scooters.Attach(s2));
        }

}
}
