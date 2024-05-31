using System;
using ElearningAPI.Datas;
namespace ElearningAPI.Repositories
{
	public interface ICommentRepository
	{
		Task <Comment> Add(Comment entity);
		Task<bool> Delete(int id);

		Task<IEnumerable<Comment>> getAll();

		Task<Comment> GetById(int id);

		Task Update(Comment entity);

		
	}
}

