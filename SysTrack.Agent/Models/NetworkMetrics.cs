namespace SysTrack.Agent.Models
{
    public class NetworkMetrics
    {
        public Dictionary<string, (int send, int receive)> NetworkUsage { get; set; } = new();
    }
}
