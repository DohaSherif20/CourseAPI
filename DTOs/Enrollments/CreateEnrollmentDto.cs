using System.ComponentModel.DataAnnotations;

namespace CourseAPI.DTOs.Enrollments
{
    public class CreateEnrollmentDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int StudentId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int CourseId { get; set; }
    }
}