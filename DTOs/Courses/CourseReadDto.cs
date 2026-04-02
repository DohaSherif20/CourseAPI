namespace CourseAPI.DTOs.Courses
{
    public class CourseReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int InstructorId { get; set; }
        public string InstructorName { get; set; }
    }
}