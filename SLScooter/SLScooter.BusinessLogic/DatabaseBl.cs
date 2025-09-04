using Microsoft.EntityFrameworkCore;
using SLScooter.DataAccess;

namespace SLScooter.BusinessLogic;

public interface DatabaseBl
{
    public static void RebuildDatabase() {
        using ScooterDbContext db = new();

        db.Database.EnsureDeleted();

        db.Database.Migrate();
    }

    // TODO: Implement ClearDatabase
    public static void ClearDatabase() {
        using ScooterDbContext db = new();
        db.Scooters.ExecuteDelete();
        db.Trips.ExecuteDelete();
        db.Users.ExecuteDelete();
    }
}