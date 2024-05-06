using ElearningAPI.DTOs;
using ElearningAPI.Models;
using static ElearningAPI.Responses.ServiceResponses;

namespace ElearningAPI.Repositories
{
	public interface IUserRepository
	{
		Task<GeneralResponse<UserModel>> Create(UserModel newUser);
		Task<LoginResponse> Login(UserModel user);
	}
}
