using System;
using System.ComponentModel.DataAnnotations;
using ElearningAPI.Datas;
namespace ElearningAPI.DTOs
{
	public class PostDTO
	{
		
		public string Description { get; set; }

		public DateTime CreatedAt { get; set; }

		public UserDTO User { get; set; }

		
         public ICollection<CommentDTO> Comments { get; set; }
       
	}
}

