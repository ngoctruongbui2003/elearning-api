using ElearningAPI.Datas;
using Microsoft.EntityFrameworkCore;

namespace ElearningAPI.Repositories.Impl
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly AppDbContext _context;

        
        public AssignmentRepository(AppDbContext context){
            _context = context;
        }
        public async Task<Assignment> Add(Assignment entity)
        {
            _context.Assignments.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
            
        }

        public async Task<bool> Delete(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);
            if(assignment != null){
                _context.Assignments.Remove(assignment);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Assignment>> GetAll()
        {
            return await _context.Assignments.ToListAsync();
        }

        public async Task<Assignment> GetById(int id)
        {
            return await _context.Assignments.FindAsync(id);
        }

        public async Task Update(Assignment entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

}