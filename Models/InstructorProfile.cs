using System.ComponentModel.DataAnnotations;

namespace CourseAPI.Models
{
    public class InstructorProfile
    {
        public int Id { get; set; }

        [MaxLength(500)]
        public string Bio { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}