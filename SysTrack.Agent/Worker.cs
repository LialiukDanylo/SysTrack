using SysTrack.Agent.Models;
using SysTrack.Agent.Monitoring;
namespace SysTrack.Agent
{
    public class Worker : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IMetricCollector metricCollector = new HardwareCollector();
            SystemMetrics metrics = new SystemMetrics();

            while (!stoppingToken.IsCancellationRequested)
            {
                metricCollector.Update();
                metrics = metricCollector.GetMetrics();
                int cpuUsage = metrics.CpuLoadPercent;
                int gpuUsage = metrics.GpuLoadPercent;
                int ramUsed = metrics.RamUsedMb;

                Console.WriteLine($"CPU Usage: {cpuUsage}%");
                Console.WriteLine($"GPU Usage: {gpuUsage}%");
                Console.WriteLine($"Ram Used: {ramUsed}Mb");

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
