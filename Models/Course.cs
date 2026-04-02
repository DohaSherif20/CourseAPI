using System.ComponentModel.DataAnnotations;

namespace CourseAPI.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Title { get; set; }

        [Required, MaxLength(500)]
        public string Description { get; set; }

        public int InstructorId { get; set; }

        public User Instructor { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}