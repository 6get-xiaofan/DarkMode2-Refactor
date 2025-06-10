using DarkMode.Core.Interfaces.Hardware;
using LibreHardwareMonitor.Hardware;
using Windows.Devices.Sensors;
using Microsoft.Extensions.Logging;

namespace DarkMode.Core.Services.Hardware;

public class HardwareService(ILogger<HardwareService> _logger) : IHardwareService, IDisposable
{
    private Computer _computer;
    private bool _isDisposed;
    
    private LightSensor _lightsensor;

    public float GetGpuUsage()
    {
        _computer = new Computer { IsGpuEnabled = true };
        _computer.Open();
        return GetGpuUtilization();
    }

    public float GetAmbientLight()
    {
        _lightsensor = LightSensor.GetDefault();
        return _lightsensor.GetCurrentReading().IlluminanceInLux;
    }


    private float GetGpuUtilization()
    {
        float maxGpuUsage = -1f;
        
        if (_computer == null) return maxGpuUsage;

        foreach (var hardware in _computer.Hardware)
        {
            hardware.Update();
            if (IsSupportedGpuType(hardware.HardwareType))
            {
                float usage = GetGpuUsageFromHardware(hardware);
                if (usage > maxGpuUsage)
                {
                    maxGpuUsage = usage;
                }
            }
        }
        
        // 过滤负值
        maxGpuUsage = Math.Max(0, maxGpuUsage);
        maxGpuUsage = Math.Min(100, maxGpuUsage);
        
        _logger.LogDebug($"Gpu usage: {maxGpuUsage}");
        
        return maxGpuUsage;
    }

    private bool IsSupportedGpuType(HardwareType hardwareType)
    {
        return hardwareType == HardwareType.GpuNvidia ||
               hardwareType == HardwareType.GpuAmd ||
               hardwareType == HardwareType.GpuIntel;
    }

    private float GetGpuUsageFromHardware(IHardware hardware)
    {
        float maxUsage = -1f;
        
        foreach (var sensor in hardware.Sensors)
        {
            if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Load && 
                sensor.Name == "GPU Core" &&
                sensor.Value.HasValue)
            {
                float currentUsage = Convert.ToSingle(sensor.Value);
                if (currentUsage > maxUsage)
                {
                    maxUsage = currentUsage;
                }
            }
        }
        
        return maxUsage;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed) return;
        
        if (disposing)
        {
            _computer?.Close();
            _computer = null;
        }

        _isDisposed = true;
    }
}