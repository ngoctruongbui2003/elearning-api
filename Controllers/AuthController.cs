using ElearningAPI.DTOs;
using ElearningAPI.Models;
using ElearningAPI.Repositories;
using ElearningAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElearningAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserService _userService;

		public AuthController(UserService userService)
		{
			_userService = userService;
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
			var res = await _userService.Create(userModel);

			if (!res.Flag) { return BadRequest(res.Message); }

			return Ok(res);
		}
		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginDTO loginDTO)
		{
			var userModel = new UserModel()
			{
				Email = loginDTO.Email,
				Password = loginDTO.Password,
			};

			var res = await _userService.Login(userModel);
			if (!res.Flag) { return BadRequest(res.Message); }

			return Ok(res);
		}
	}
}
