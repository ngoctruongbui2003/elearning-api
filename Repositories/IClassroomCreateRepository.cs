using System;
using ElearningAPI.Datas;
namespace ElearningAPI.Repositories.Impl
{
	public interface IClassroomCreateRepository
	{
		Task<bool> Add(ClassroomCreate entity);

		Task<IEnumerable<ClassroomCreate>> GetById(int classroomId);

		Task<ClassroomCreate> GetByUser(string userId,int classroomId);

		

		
	}
}

