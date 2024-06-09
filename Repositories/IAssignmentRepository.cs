using ElearningAPI.Datas;

namespace ElearningAPI.Repositories
{
    public interface IAssignmentRepository
    {
        Task<IEnumerable<Assignment>> GetAll();

        Task<Assignment> Add(Assignment entity);

        Task<bool> Delete(int id);

        Task Update(Assignment entity);

        Task<Assignment> GetById(int id);
    }
    
}