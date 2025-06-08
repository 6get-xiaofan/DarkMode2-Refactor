namespace DarkMode.Core.Interfaces.Location;

/**
 * 位置
 */
public interface ILocationService
{
    Task<(double lat, double lon)> GetCurrentLocationAsync();
}