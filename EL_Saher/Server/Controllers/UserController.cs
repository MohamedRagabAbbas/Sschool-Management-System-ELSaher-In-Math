using EL_Saher.Server.Services;
using EL_Saher.Shared.Models;
using EL_Saher.Shared.Models.ServiceModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EL_Saher.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserServices _userServices;
		public UserController(IUserServices userServices)
		{
			_userServices = userServices;
		}
		[HttpPost("Register")]
		public async Task<ServiceResponse<string>> Register(UserInfo newUser)
		{
			return await _userServices.AddNewUser(newUser);
		}
		[HttpPost("Login")]
		public async Task<IActionResult> Login(UserLogInInfo _user)
		{
			var message = await _userServices.Login(_user);
			return Ok(message);
		}
	}
}
