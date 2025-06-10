using SysTrack.API.Services.Interfaces;

namespace SysTrack.API.Services
{
    public class ConnectionManager : IConnectionManager
    {
        private readonly Dictionary<string, HashSet<string>> _groups = new();
        private readonly Dictionary<string, string> _connectionToGroup = new();
        private readonly object _lock = new();

        public void AddClient(string groupId, string connectionId)
        {
            lock (_lock)
            {
                if (!_groups.ContainsKey(groupId))
                    _groups[groupId] = new HashSet<string>();

                _groups[groupId].Add(connectionId);
                _connectionToGroup[connectionId] = groupId;
            }
        }

        public void RemoveClient(string connectionId)
        {
            lock (_lock)
            {
                if (_connectionToGroup.TryGetValue(connectionId, out var groupId))
                {
                    if (_groups.TryGetValue(groupId, out var clients))
                    {
                        Console.WriteLine($"[{connectionId}] client disconnected from room {groupId}");
                        clients.Remove(connectionId);

                        if (clients.Count == 0)
                            _groups.Remove(groupId);
                    }
                    else
                    {
                        Console.WriteLine($"[{connectionId}] agent disconnected from room {groupId}");
                    }

                    _connectionToGroup.Remove(connectionId);
                }
            }
        }

        public bool IsClient(string connectionId)
        {
            lock (_lock)
            {
                return _connectionToGroup.ContainsKey(connectionId);
            }
        }

        public string GetGroupId(string connectionId)
        {
            lock (_lock)
            {
                if(_connectionToGroup.TryGetValue(connectionId, out var groupId))
                {
                    return groupId;
                }
                return "";
            }
        }

        public int GetClientsCount(string groupId)
        {
            lock (_lock)
            {
                if(_groups.TryGetValue(groupId, out var clients))
                {
                    return clients.Count;
                }

                return 0;
            }
        }
    }
}