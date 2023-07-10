using EL_Saher.Server.DataBase;
using EL_Saher.Shared.Models;
using EL_Saher.Shared.Models.ServiceModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EL_Saher.Server.Services
{
	public class UserServices : IUserServices
	{
		private readonly DbContextClass dbContext;

		private readonly IConfiguration configuration;

        public UserServices(DbContextClass _dbContext, IConfiguration _configuration)
		{
			dbContext = _dbContext;
            configuration = _configuration;
        }
		public async Task<ServiceResponse<string>> AddNewUser(UserInfo newUser)
		{
			if ((IsExist(newUser.UserName).Result))
			{
				var response = new ServiceResponse<string>();
                response.IsSuccess = false;
				response.Message = "هذا المستخدم موجود بالفعل";
				return response;
			}
			else
			{
				CreatePassword(newUser.Password, out byte[] PasswordHash, out byte[] Passwordsalt);
				var user = new User
				{
					UserName = newUser.UserName,
					IsAdmin = newUser.IsAdmin,
					PasswordHash = PasswordHash,
					Passwordsalt = Passwordsalt,
					DateCreated = System.DateTime.Now,
				};
				await dbContext.Users.AddAsync(user);
				await dbContext.SaveChangesAsync();
                var response = new ServiceResponse<string>();
				response.IsSuccess = true;
				response.Message = "تم اضافة المستخدم بنجاح";
				response.Data = CreateToken(user);
				return response;
			}
		}
		public async Task <bool> IsExist(string UserName)
		{
			return await dbContext.Users.AnyAsync(x => x.UserName == UserName);
		}
		public void CreatePassword(string Password, out byte[] PasswordHash, out byte[] Passwordsalt)
		{
			using (var hmac = new HMACSHA512())
			{
				Passwordsalt = hmac.Key;
				PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
			}
		}
		public async Task<ServiceResponse<string>> Login(UserLogInInfo _user)
		{
			var user = await dbContext.Users.FirstOrDefaultAsync(x => x.UserName == _user.UserName);
			if (user != null)
			{
				if (VerifyPasswordHash(_user.Password, user.PasswordHash, user.Passwordsalt))
				{
					return new ServiceResponse<string>() {  Data =CreateToken(user), IsSuccess =true,};
				}
				else
				{
                    return new ServiceResponse<string>() { IsSuccess = false, Message = "كلمة السر غير صحيحة" };
				}
			}
			else
			{
                return new ServiceResponse<string>() { IsSuccess = false, Message = "هذا المستخدم غير موجود." };
			}
		}
		public bool VerifyPasswordHash(string Password, byte[] PasswordHash, byte[] Passwordsalt)
		{
			using (var hmac = new HMACSHA512(Passwordsalt))
			{
				var ComputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
				return ComputedHash.SequenceEqual(PasswordHash);
			}
		}

		private string CreateToken(User user)
		{
            var claims = new List<Claim>
			{
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
			{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

	}
}
