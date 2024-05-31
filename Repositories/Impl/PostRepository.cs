using System;
using ElearningAPI.Datas;
using Microsoft.EntityFrameworkCore;
namespace ElearningAPI.Repositories.Impl
{
	public class PostRepository : IPostRepository
	{
		private readonly AppDbContext _context;
       
		public PostRepository(AppDbContext context)
		{
            _context = context;
		}

        public async Task<Post> Add(Post entity)
        {
            _context.Posts.Add(entity);
            await _context.SaveChangesAsync();
            return entity; 
        }

        public async Task<bool> Delete(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if(post != null){
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<Post> GetById(int id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public async Task Update(Post entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

