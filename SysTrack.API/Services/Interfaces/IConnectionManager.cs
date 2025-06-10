namespace SysTrack.API.Services.Interfaces
{
    public interface IConnectionManager
    {
        void AddClient(string groupId, string connectionId);
        void RemoveClient(string connectionId);
        bool IsClient(string connectionId);
        string GetGroupId(string connectionId);
        int GetClientsCount(string groupId);
    }
}
