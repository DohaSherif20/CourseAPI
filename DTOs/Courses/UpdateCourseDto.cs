using System.ComponentModel.DataAnnotations;

namespace CourseAPI.DTOs.Courses
{
    public class UpdateCourseDto
    {
        [Required]
        [MaxLength(150)]
        [MinLength(3)]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        [MinLength(5)]
        public string Description { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int InstructorId { get; set; }
    }
}