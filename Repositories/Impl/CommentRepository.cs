using System;
using ElearningAPI.Datas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
namespace ElearningAPI.Repositories.Impl
{
    public class CommentRepository : ICommentRepository
    {
		private readonly AppDbContext _context;
        public CommentRepository(AppDbContext context){
            _context = context;
        }
        public async Task<Comment> Add(Comment entity)
        {
            _context.Comments.Add(entity);
			await _context.SaveChangesAsync();
;			return entity;
        }

        public async Task<bool> Delete(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if(comment != null){
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Comment>> getAll()
        {
            return await _context.Comments.Include(x=>x.User).ToListAsync();
        }

        public async Task<Comment> GetById(int id)
        {
			return await _context.Comments.FindAsync(id);
        }

        public async Task Update(Comment entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
			await _context.SaveChangesAsync();
        }
    }
}

