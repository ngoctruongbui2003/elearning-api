using ElearningAPI.DTOs;
using ElearningAPI.Models;
using ElearningAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElearningAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IUserRepository _userRepository;

		public AuthController(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterDTO register)
		{
			UserModel userModel = new UserModel()
			{
				FullName = register.FullName,
				Email = register.Email,
				Password = register.Password,
			};
			var response = await _userRepository.Create(userModel);

			if (!response.Flag) { return BadRequest(response.Message); }

			return Ok(response);
		}
		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginDTO loginDTO)
		{
			var userModel = new UserModel()
			{
				Email = loginDTO.Email,
				Password = loginDTO.Password,
			};

			var res = await _userRepository.Login(userModel);
			if (!res.Flag) { return BadRequest(res.Message); }

			return Ok(res);
		}
	}
}
