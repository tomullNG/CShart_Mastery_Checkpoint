namespace SLScooter.Domain.Entity;

public class Trip
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; } // TODO: Should be null if not ended yet
    public int Distance { get; set; } // km
    public double Cost { get; set; } // NOK
    public int UserId { get; set; }
    public int ScooterId { get; set; }
}
