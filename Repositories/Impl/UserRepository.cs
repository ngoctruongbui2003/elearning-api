using ElearningAPI.Datas;
using ElearningAPI.DTOs;
using ElearningAPI.Models;
using ElearningAPI.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static ElearningAPI.Responses.ServiceResponses;

namespace ElearningAPI.Repositories.Impl
{
	public class UserRepository : IUserRepository
	{
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IConfiguration _config;

		public UserRepository(
			UserManager<User> userManager,
			RoleManager<IdentityRole> roleManager,
			IConfiguration config
		)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_config = config;
		}

		public async Task<GeneralResponse<UserModel>> Create(UserModel model)
		{
			// ---- Check model input ----
			if (model == null) return new GeneralResponse<UserModel>(false, "Model user is empty!", null);

			var newUser = new User(){
				FullName = model.FullName,
				Email = model.Email,
				PasswordHash = model.Password,
				UserName = model.Email
			};

			// ---- Check email exists ----
			var user = await _userManager.FindByEmailAsync(newUser.Email);
			if (user != null) return new GeneralResponse<UserModel>(false, "User registered already", null);

			// ---- Create user ----
			var createUser = await _userManager.CreateAsync(newUser!, model.Password);
			if (!createUser.Succeeded) return new GeneralResponse<UserModel>(false, "Error occurred.. please try again", null);

			var newUserModel = new UserModel()
			{
				Id = newUser.Id,
				FullName = newUser.FullName,
				Email = newUser.Email,
				Password = newUser.PasswordHash,
			};

			var checkAdmin = await _roleManager.FindByNameAsync("Admin");
			if (checkAdmin == null)
			{
				await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
				await _userManager.AddToRoleAsync(newUser, "Admin");

				return new GeneralResponse<UserModel>(true, "Account Created", newUserModel);
			}

			var checkUser = await _roleManager.FindByNameAsync("User");
			if (checkUser == null)
				await _roleManager.CreateAsync(new IdentityRole() { Name = "User" });
			await _userManager.AddToRoleAsync(newUser, "User");

			return new GeneralResponse<UserModel>(true, "Account Created", newUserModel);
		}

		public async Task<LoginResponse> Login(UserModel model)
		{
			if (model == null)
				return new LoginResponse(false, null!, "Login container is empty");

			var getUser = await _userManager.FindByEmailAsync(model.Email);
			if (getUser is null)
				return new LoginResponse(false, null!, "User not found");

			bool checkUserPasswords = await _userManager.CheckPasswordAsync(getUser, model.Password);
			if (!checkUserPasswords)
				return new LoginResponse(false, null!, "Invalid email/password");

			var getUserRole = await _userManager.GetRolesAsync(getUser);
			var userSession = new UserSession(getUser.Id, getUser.FullName, getUser.Email, getUserRole.First());
			string token = GenerateToken(userSession);
			return new LoginResponse(true, token!, "Login completed");
		}

		private string GenerateToken(UserSession user)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.Aes128CbcHmacSha256);
			var userClaims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id),
				new Claim(ClaimTypes.Name, user.FullName),
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.Role, user.Role)
			};
			var token = new JwtSecurityToken(
				issuer: _config["Jwt:Issuer"],
				audience: _config["Jwt:Audience"],
				claims: userClaims,
				expires: DateTime.Now.AddDays(1),
				signingCredentials: credentials
				);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
