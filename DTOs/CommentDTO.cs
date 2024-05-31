using System;
namespace ElearningAPI.DTOs
{
	public class CommentDTO
	{
		public string Description { get ; set ;}
		public DateTime CreatedAt { get; set; }
    	public UserDTO User { get; set; }
	}
}

