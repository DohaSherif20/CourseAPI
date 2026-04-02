using System.ComponentModel.DataAnnotations;

namespace CourseAPI.DTOs.InstructorProfiles
{
    public class CreateInstructorProfileDto
    {
        [Required]
        [MaxLength(500)]
        [MinLength(5)]
        public string Bio { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }
    }
}