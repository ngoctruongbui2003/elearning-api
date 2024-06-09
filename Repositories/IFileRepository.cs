using System;
using ElearningAPI.Datas;
namespace ElearningAPI.Repositories.Impl
{
	public interface IFileRepository
	{
		Task<UploadFile> GetById(int id);

		Task<IEnumerable<UploadFile>> GetAll();

		
		

	}
}

