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
	}
}
