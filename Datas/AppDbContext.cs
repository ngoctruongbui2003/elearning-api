using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ElearningAPI.Datas
{
	public class AppDbContext : IdentityDbContext<User>
	{
		public AppDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Classroom>? Classrooms { get; set; }

		public DbSet<ClassroomCreate>? ClassroomCreates{get; set;}

		public DbSet<Post>? Posts{ get; set; }

		public DbSet<Comment>? Comments { get; set; }
	}
}
