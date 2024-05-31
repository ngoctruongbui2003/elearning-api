using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
namespace ElearningAPI.Datas
{
	public class Comment
	{
		[Key]
		public int Id { get; set; }

		public string Description { get; set; }

		public Boolean IsDelete { get; set; }

		public DateTime CreatedAt { get; set; }
		public string UserId { get; set; }

		public int PostId{ get; set; }

		public Post Post{ get; set; }

		public User User{ get; set; }
	}
}

