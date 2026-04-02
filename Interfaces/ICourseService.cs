using CourseAPI.DTOs.Courses;

namespace CourseAPI.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseReadDto>> GetAllAsync();
        Task<CourseReadDto?> GetByIdAsync(int id);
        Task<CourseReadDto?> CreateAsync(CreateCourseDto dto);
        Task<bool> UpdateAsync(int id, UpdateCourseDto dto);
        Task<bool> DeleteAsync(int id);
    }
}