using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using SysTrack.Client.Services.Interfaces;
using SysTrack.Shared.Models;

namespace SysTrack.Client.Services
{
    internal class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        public AuthService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }
        public async Task<LoginResponse> SignIn(LoginRequest request)
        {
            var jsonRequest = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync("http://localhost:5265/api/auth/login", content);

                var responseContent = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                return JsonSerializer.Deserialize<LoginResponse>(responseContent, options);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exeption: {ex}");
                return null;
            }
        }
    }
}
