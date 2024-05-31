using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace ElearningAPI.Datas
{
	public class ClassroomCreate
	{
		[Key]
        public int Id { get; set; }
        public Boolean? IsTeacher { get; set; }
        public Boolean? IsExit { get; set; }


        public int ClassroomId { get; set; }

		public string? UserId {get; set;}

        public Classroom Classroom { get; set; } = null!;
        public User User { get; set; } = null!;
	}
}

