namespace DarkMode.Core.Interfaces.Location;

public interface ILocationService
{
    Task<(double lat, double lon)> GetCurrentLocationAsync();
}