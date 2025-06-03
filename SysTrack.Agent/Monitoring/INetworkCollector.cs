using SysTrack.Agent.Models;

namespace SysTrack.Agent.Monitoring
{
    public interface INetworkCollector
    {
        public void Update();
        public NetworkMetrics GetNetworkUsage();
    }
}
