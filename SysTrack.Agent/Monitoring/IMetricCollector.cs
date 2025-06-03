using SysTrack.Agent.Models;

namespace SysTrack.Agent.Monitoring
{
    public interface IMetricCollector
    {
        public void Update();

        public SystemMetrics GetMetrics();
    }
}
