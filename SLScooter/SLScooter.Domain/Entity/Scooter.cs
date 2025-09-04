namespace SLScooter.Domain.Entity
{
    public class Scooter
    {
        public int Id { get; set; }
        public string Brand { get; set; } // Could have been enum if strict list of brands or class if brand had more properties
        public int BatteryCapacity { get; set; }
        public ScooterStatus Status { get; set; }
        public ICollection<Trip>? Trips { get; set; }
    }
}
