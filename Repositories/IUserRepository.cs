using ElearningAPI.Datas;

namespace ElearningAPI.Repositories
{
	public interface IUserRepository
	{
		Task<User> GetById(string id);
		Task<User> GetByEmail(string email);
		Task<IEnumerable<User>> GetAll();
		Task<string> GetRoleByUser(User user);
		Task<bool> Add(User entity, string password);
		Task<bool> AddRoleToUser(User user, string rollName);
		Task<bool> AddRole(string roleName);
		Task Update(User entity);
		Task Delete(User entity);
		Task<bool> IsRoleExists(string roleName);
		Task<bool> CheckPassword(User user, string password);

		Task<IEnumerable<User>> GetAllById(IEnumerable<string> userId);

		
	}
}
