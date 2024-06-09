using System.ComponentModel.DataAnnotations;

namespace ElearningAPI.DTOs
{
	public class ClassroomDTO
	{
		
		public string? Code { get; set; }

		public string? Name { get; set; } = null!;

		public string? Description { get; set; } = null!;

		


		
	}
}
