using System;
using ElearningAPI.Datas;
using Microsoft.EntityFrameworkCore;
namespace ElearningAPI.Repositories.Impl
{
    public class ClassroomCreateRepository : IClassroomCreateRepository
    {
		private readonly AppDbContext _context;
		public ClassroomCreateRepository(AppDbContext context){
			_context = context;
		}
        public async Task<bool> Add(ClassroomCreate entity)
        {
            _context.ClassroomCreates.Add(entity);
			await _context.SaveChangesAsync();
			return true;
        }

        public async Task<IEnumerable<ClassroomCreate>> GetById(int classroomId)
        {
            return await _context.ClassroomCreates.Where(x => x.ClassroomId == classroomId).ToListAsync();
        }

        public async Task<ClassroomCreate> GetByUser(string userId, int classroomId)
        {
            return await _context.ClassroomCreates.FirstOrDefaultAsync(x => x.UserId == userId && x.ClassroomId == classroomId);
        }
    }
}

