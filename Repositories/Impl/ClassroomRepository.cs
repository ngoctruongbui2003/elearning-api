using ElearningAPI.Datas;
using Microsoft.EntityFrameworkCore;

namespace ElearningAPI.Repositories.Impl
{
	public class ClassroomRepository : IClassroomRepository
	{
		private readonly AppDbContext _context;

        public ClassroomRepository(AppDbContext context){
            _context = context;
        }

        public async Task<bool> Add(Classroom entity)
        {
            _context.Classrooms.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var classroom = await _context.Classrooms.FindAsync(id);
            if(classroom != null){
                _context.Classrooms.Remove(classroom);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public Task<bool> Exit(int id)
        {
            return null;
        }

        public async Task<IEnumerable<Classroom>> GetAll()
        {
            return await _context.Classrooms.ToListAsync();
        }

        public async Task<IEnumerable<Classroom>> GetAllByUser()
        {
            
            return null;
        }

        public async Task<Classroom> GetById(int id)
        {
            return await _context.Classrooms.FindAsync(id);
        }

        public async Task<Classroom> GetByCode(String code)
        {
            
            return await _context.Classrooms.FirstOrDefaultAsync(c => c.Code == code);
        }

        public async Task Update(Classroom entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
	}
}
