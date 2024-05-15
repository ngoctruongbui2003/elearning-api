using ElearningAPI.Datas;
using ElearningAPI.Models;
using ElearningAPI.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using static ElearningAPI.Responses.ServiceResponses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ElearningAPI.Repositories;

namespace ElearningAPI.Services
{
	public class UserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IConfiguration _config;

		public UserService(IUserRepository userRepository, IConfiguration config)
		{
			_userRepository = userRepository;
			_config = config;
		}

		public async Task<GeneralResponse<UserModel>> Create(UserModel model)
		{
			// ---- Check model input ----
			if (model == null) return new GeneralResponse<UserModel>(false, "Model user is empty!", null);

			var newUser = new User()
			{
				FullName = model.FullName,
				Email = model.Email,
				PasswordHash = model.Password,
				UserName = model.Email
			};

			// ---- Check email exists ----
			var user = await _userRepository.GetByEmail(newUser.Email);
			if (user != null) return new GeneralResponse<UserModel>(false, "User registered already", null);

			// ---- Create user ----
			var isSuccessAdd = await _userRepository.Add(newUser!, model.Password);
			if (!isSuccessAdd) return new GeneralResponse<UserModel>(false, "Error occurred.. please try again", null);

			var newUserModel = new UserModel()
			{
				Id = newUser.Id,
				FullName = newUser.FullName,
				Email = newUser.Email,
				Password = newUser.PasswordHash,
			};

			var isAdminExists = await _userRepository.IsRoleExists("Admin");
			if (!isAdminExists)
			{
				isSuccessAdd = await _userRepository.AddRole("Admin");
				if (!isSuccessAdd) return new GeneralResponse<UserModel>(false, "Error occurred.. please try again", null);

				isSuccessAdd = await _userRepository.AddRoleToUser(newUser, "Admin");
				if (!isSuccessAdd) return new GeneralResponse<UserModel>(false, "Error occurred.. please try again", null);

				return new GeneralResponse<UserModel>(true, "Account Created", newUserModel);
			}

			var isUserExists = await _userRepository.IsRoleExists("User");
			if (!isUserExists)
			{
				isSuccessAdd = await _userRepository.AddRole("User");
				if (!isSuccessAdd) return new GeneralResponse<UserModel>(false, "Error occurred.. please try again", null);
			}

			isSuccessAdd = await _userRepository.AddRoleToUser(newUser, "User");
			if (!isSuccessAdd) return new GeneralResponse<UserModel>(false, "Error occurred.. please try again", null);

			return new GeneralResponse<UserModel>(true, "Account Created", newUserModel);
		}

		public async Task<LoginResponse> Login(UserModel model)
		{
			if (model == null)
				return new LoginResponse(false, null!, "Login container is empty");

			var getUser = await _userRepository.GetByEmail(model.Email);
			if (getUser == null)
				return new LoginResponse(false, null!, "User not found");

			bool checkUserPasswords = await _userRepository.CheckPassword(getUser, model.Password);
			if (!checkUserPasswords)
				return new LoginResponse(false, null!, "Invalid email/password");

			var userRole = await _userRepository.GetRoleByUser(getUser);
			var userSession = new UserSession(getUser.Id, getUser.FullName, getUser.Email, userRole);
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
