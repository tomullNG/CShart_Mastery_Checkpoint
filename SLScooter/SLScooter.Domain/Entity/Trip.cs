namespace SLScooter.Domain.Entity;

public class Trip
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; } // TODO: Should be null if not ended yet
    public int Distance { get; set; } // km
    public decimal Cost { get; set; } // NOK
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int ScooterId { get; set; }
    public Scooter Scooter { get; set; } = null!;
}
