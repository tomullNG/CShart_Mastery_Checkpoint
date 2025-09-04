namespace SLScooter.Domain.Entity;

public class User(string name, int phoneNumber)
{
    public int Id { get; set; }
    public string Name { get; set; } = name;
    public int PhoneNumber { get; set; } = phoneNumber;
    public ICollection<Trip>? Trips { get; set; } = new List<Trip>();
}