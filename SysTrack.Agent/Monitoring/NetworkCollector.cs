using System.Diagnostics;
using System.Net.NetworkInformation;
using SysTrack.Agent.Models;

namespace SysTrack.Agent.Monitoring
{
    public class NetworkCollector : INetworkCollector
    {
        private readonly Dictionary<string, (PerformanceCounter send, PerformanceCounter receive)> _adapters = new();
        private NetworkMetrics _metrics = new NetworkMetrics();

        public NetworkCollector() 
        {
            var instances = new PerformanceCounterCategory("Network Interface").GetInstanceNames();

            foreach (var name in instances)
            {
                var ni = NetworkInterface.GetAllNetworkInterfaces()
                                         .FirstOrDefault(n => n.Description == name);

                if (ni != null && ni.OperationalStatus == OperationalStatus.Up)
                {
                    var sendCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", name);
                    var recvCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", name);

                    _adapters[name] = (sendCounter, recvCounter);
                }
            }
        }
        public void Update()
        {
            foreach (var adapter in _adapters)
            {
                int send = (int)adapter.Value.send.NextValue();
                int receive = (int)adapter.Value.receive.NextValue();

                _metrics.NetworkUsage[adapter.Key] = (send, receive);
            }
        }

        public NetworkMetrics GetNetworkUsage()
        {
            return _metrics;
        }
    }
}
