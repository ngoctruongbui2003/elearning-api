using Microsoft.AspNetCore.Identity;

namespace ElearningAPI.Datas
{
	public class User : IdentityUser
	{
		public string? FullName { get; set; }
<<<<<<< HEAD
		
=======

		public ICollection<ClassroomCreate> ClassroomCreates {get; set;}
		// 1-n
		public ICollection<Post> Posts { get; set; }
		// 1-n
		public ICollection<Comment> Comments { get; set; }


>>>>>>> 85eb5e8 (code post + comment)
	}
}
