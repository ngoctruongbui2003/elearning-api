namespace ElearningAPI.DTOs
{
	public class ClassroomDTO
	{
		public int Id { get; set; }
		public string? Code { get; set; }

		public string? Name { get; set; } = null!;

		public string? Description { get; set; } = null!;

		public string? Type { get; set; } = null!;
		public bool IsTurnOnCode { get; set; } = true;
		public bool IsDeleted { get; set; } = false;
	}
}
