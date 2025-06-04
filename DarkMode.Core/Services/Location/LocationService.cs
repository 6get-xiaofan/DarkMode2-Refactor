using DarkMode.Core.Interfaces.Location;

namespace DarkMode.Core.Services.Location;

public class LocationService : ILocationService
{
    public Task<(double lat, double lon)> GetCurrentLocationAsync() => throw new NotImplementedException();
}