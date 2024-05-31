using ElearningAPI.Datas;
using ElearningAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static ElearningAPI.Responses.ServiceResponses;

namespace ElearningAPI.Repositories.Impl
{
	public class UserRepository : IUserRepository
	{
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		private readonly AppDbContext _appDbContext;
		public UserRepository(
			UserManager<User> userManager,
			RoleManager<IdentityRole> roleManager,
			AppDbContext appDbContext
		)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_appDbContext = appDbContext;
		}
		public async Task<bool> Add(User entity, string password)
		{
			var createUser = await _userManager.CreateAsync(entity!, password);

			return createUser.Succeeded;
		}

		public async Task<bool> AddRole(string roleName)
		{
			var createRole = await _roleManager.CreateAsync(new IdentityRole() { Name = roleName });

			return createRole.Succeeded;
		}

		public async Task<bool> AddRoleToUser(User user, string rollName)
		{
			var addRole = await _userManager.AddToRoleAsync(user, rollName);

			return addRole.Succeeded;
		}

		public async Task<bool> CheckPassword(User user, string password)
		{
			return await _userManager.CheckPasswordAsync(user, password);
		}

		public Task Delete(User entity)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<User>> GetAll()
		{
			return await _appDbContext.Users.ToListAsync();
		}

        public async Task<IEnumerable<User>> GetAllById(IEnumerable<string> userId)
        {
            return await _appDbContext.Users.Where(x => userId.Contains(x.Id)).ToListAsync();
        }

        public async Task<User> GetByEmail(string email)
		{
			return await _userManager.FindByEmailAsync(email);
		}

		public async Task<User> GetById(string id)
		{
			return await _appDbContext.Users.FindAsync(id);
		}

		public async Task<string> GetRoleByUser(User user)
		{
			var getUserRole = await _userManager.GetRolesAsync(user);
			return getUserRole.First();
		}

		public async Task<bool> IsRoleExists(string roleName)
		{
			var checkAdmin = await _roleManager.FindByNameAsync(roleName);

			return checkAdmin != null;
		}

		public Task Update(User entity)
		{
			throw new NotImplementedException();
		}
	}
}
