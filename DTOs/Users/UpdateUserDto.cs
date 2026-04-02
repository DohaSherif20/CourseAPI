using System.ComponentModel.DataAnnotations;

namespace CourseAPI.DTOs.Users
{
    public class UpdateUserDto
    {
        [Required]
        [MaxLength(100)]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression("Admin|Instructor|Student", ErrorMessage = "Role must be Admin, Instructor, or Student.")]
        public string Role { get; set; }
    }
}