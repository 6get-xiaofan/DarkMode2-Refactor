using DarkMode.Core.Interfaces.Hardware;

namespace DarkMode.Core.Services.Hardware;

public class HardwareService : IHardwareService
{
    public float GetGpuUsage() => throw new NotImplementedException();

    public float GetAmbientLight() => throw new NotImplementedException();
}