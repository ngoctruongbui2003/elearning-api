using System;
using System.ComponentModel.DataAnnotations;
namespace ElearningAPI.Datas
{
	public class Post
	{
		[Key]
		public int Id {get ; set ;}

		public String? Description {get ; set ;}

		public DateTime CreatedAt { get; set; }

        public Boolean IsDelete { get; set; }

        // Relationship 1-1
        // public ApplicationUser ApplicationUser { get; set; }
        public String? UserId { get ; set; }
        // n - 1
        public int ClassroomId { get; set; }

		public int CommentId { get; set; }
        public Classroom Classroom { get; set; } = null!;

		public User User { get; set; } = null!;

        // Relationship 1-n with Comment
        public ICollection<Comment> Comments { get; set; }

       
	}
}

