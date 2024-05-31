using ElearningAPI.DTOs;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace ElearningAPI.Datas
{
	public class Classroom
	{
		[Key]
		public int Id { get; set; }
		public string? Code { get; set; }

		public string? Name { get; set; } = null!;

		public string? Description { get; set; } = null!;

		public string? Type { get; set; } = null!;
		public string? CreatedUser { get; set; } = null!;
		public DateTime CreatedAt { get; set; }
		public bool IsTurnOnCode { get; set; } = true;
		public bool IsDeleted { get; set; } = false;


		// 1-n
		public ICollection<ClassroomCreate> ClassroomCreates { get; set; } 

		// 1-n
		public ICollection<Post> Posts { get; set; }
		
		public Classroom(){}
		public Classroom(ClassroomDTO classroomDTO){
			Code = classroomDTO.Code;
			Name = classroomDTO.Name;
			Description = classroomDTO.Description;
		}
	}
}
