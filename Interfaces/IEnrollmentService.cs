using CourseAPI.DTOs.Enrollments;

namespace CourseAPI.Interfaces
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<EnrollmentReadDto>> GetAllAsync();
        Task<EnrollmentReadDto?> GetByIdAsync(int id);
        Task<EnrollmentReadDto?> CreateAsync(CreateEnrollmentDto dto);
        Task<bool> DeleteAsync(int id);
    }
}