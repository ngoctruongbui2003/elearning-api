using System;
using System.ComponentModel.DataAnnotations;
namespace ElearningAPI.Datas
{
	public class UploadFile
	{
		[Key]

		public int Id{ get ; set; }
		public string FileName {get ; set; }

		public string FilePath { get; set ;}

		public int ClassroomId { get ; set;}

		public Classroom Classroom { get ; set ;}

		

	}
}

