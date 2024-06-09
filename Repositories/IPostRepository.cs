using System;
using ElearningAPI.Datas;
namespace ElearningAPI.Repositories.Impl
{
	public interface IPostRepository
	{
		Task<Post> GetById(int id);

		Task<IEnumerable<Post>> GetByIdClassroom(int id);

		Task<IEnumerable<Post>> GetAll();
		Task<Post> Add(Post entity);

		Task<bool> Delete(int id);

		Task Update(Post entity);

		
	}
}

