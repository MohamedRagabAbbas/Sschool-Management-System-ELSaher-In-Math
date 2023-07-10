using EL_Saher.Shared.Models;
using EL_Saher.Shared.Models.ServiceModels;

namespace EL_Saher.Server.Services
{
    public interface IUserServices
	{
		public Task<ServiceResponse<string>> AddNewUser(UserInfo newUser);
		public Task<bool> IsExist(string UserName);
		public void CreatePassword(string Password, out byte[] PasswordHash, out byte[] Passwordsalt);
		public  Task<ServiceResponse<string>> Login(UserLogInInfo _user);
		public bool VerifyPasswordHash(string Password, byte[] PasswordHash, byte[] Passwordsalt);

	}
}
