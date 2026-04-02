using System.ComponentModel.DataAnnotations;

namespace CourseAPI.DTOs.InstructorProfiles
{
    public class UpdateInstructorProfileDto
    {
        [Required]
        [MaxLength(500)]
        [MinLength(5)]
        public string Bio { get; set; }
    }
}