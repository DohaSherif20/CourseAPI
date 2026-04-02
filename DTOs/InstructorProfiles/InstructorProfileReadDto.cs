namespace CourseAPI.DTOs.InstructorProfiles
{
    public class InstructorProfileReadDto
    {
        public int Id { get; set; }
        public string Bio { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}