using SysTrack.Shared.Models;

namespace SysTrack.Client.Services.Interfaces
{
    internal interface IAuthService
    {
        Task<LoginResponse> SignIn(LoginRequest request);
    }
}
