using System.ComponentModel.DataAnnotations;


namespace ElearningAPI.Datas
{
    public class Assignment
    {
        [Key]
        public int Id{  get; set ;}

        public string? Title { get ; set ;}

        public string? Description { get; set; }

        public DateTime DueDate { get ; set ; }

        public int ClassroomId {get;  set ;}

        public String? UserId { get ; set; }

        public Classroom? Classroom { get ; set; }

        public User? User {get ; set ;}


        
    }
    
}