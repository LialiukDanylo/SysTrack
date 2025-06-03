using SysTrack.Agent.Models;
using LibreHardwareMonitor.Hardware;

namespace SysTrack.Agent.Monitoring
{
    public class HardwareCollector : IMetricCollector
    {
        private HardwareMetrics _metrics = new HardwareMetrics();
        private readonly Computer _computer = new Computer()
        {
            IsCpuEnabled = true,
            IsGpuEnabled = true,
            IsMemoryEnabled = true
        };

        public void Update()
        {
            _computer.Open();

            foreach (var hw in _computer.Hardware)
            {
                hw.Update();

                foreach (var sensor in hw.Sensors)
                {
                    if (sensor.Name == "CPU Total")
                    {
                        _metrics.CpuLoadPercent = (int)(sensor.Value ?? 0);
                    } 
                    else if (sensor.Name == "GPU Core")
                    {
                        _metrics.GpuLoadPercent = (int)(sensor.Value ?? 0);
                    }
                    else if (sensor.Name == "Memory Used")
                    {
                        _metrics.RamUsedMb = (int)((sensor.Value ?? 0) * 1024);
                    }
                }
            }
        }

        public HardwareMetrics GetMetrics()
        {
            return _metrics;
        }
    }
}
