using ElearningAPI.Datas;

namespace ElearningAPI.Repositories
{
	public interface IClassroomRepository
	{
		Task<Classroom> GetById(int id);
		Task<IEnumerable<Classroom>> GetAll();
		Task<IEnumerable<Classroom>> GetAllByUser();
		Task<bool> Add(Classroom entity);
		Task Update(Classroom entity);
		Task Delete(Classroom entity);
	}
}
