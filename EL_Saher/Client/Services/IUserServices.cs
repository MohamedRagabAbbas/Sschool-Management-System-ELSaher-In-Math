using EL_Saher.Shared.Models;
using EL_Saher.Shared.Models.ServiceModels;

namespace EL_Saher.Client.Services
{
    public interface IUserServices
    {
        Task<ServiceResponse<string>> Register( UserInfo request);
        Task<ServiceResponse<string>> Login(UserLogInInfo request);

    }
}
