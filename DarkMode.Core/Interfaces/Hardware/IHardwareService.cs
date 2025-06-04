namespace DarkMode.Core.Interfaces.Hardware;

public interface IHardwareService
{
    float GetGpuUsage();
    float GetAmbientLight();
}