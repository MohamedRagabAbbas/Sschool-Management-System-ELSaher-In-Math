using EL_Saher.Shared.Models;
using EL_Saher.Shared.Models.ServiceModels;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Text.Json;

namespace EL_Saher.Client.Services
{

    public class UserServices : IUserServices
    {
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;

        public UserServices(HttpClient http, AuthenticationStateProvider authStateProvider)
        {
            _http = http;
            _authStateProvider = authStateProvider;
        }

        public async Task<ServiceResponse<string>> Login(UserLogInInfo request)
        {
            var result = await _http.PostAsJsonAsync<UserLogInInfo>("api/User/Login", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<string>> Register(UserInfo request)
        {
            var result = await _http.PostAsJsonAsync<UserInfo>("api/auth/Register", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }
    }
}
