namespace SysTrack.Shared.Models
{
    public record LoginRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
