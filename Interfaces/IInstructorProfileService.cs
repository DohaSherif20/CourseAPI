using CourseAPI.DTOs.InstructorProfiles;

namespace CourseAPI.Interfaces
{
    public interface IInstructorProfileService
    {
        Task<IEnumerable<InstructorProfileReadDto>> GetAllAsync();
        Task<InstructorProfileReadDto?> GetByIdAsync(int id);
        Task<InstructorProfileReadDto?> CreateAsync(CreateInstructorProfileDto dto);
        Task<bool> UpdateAsync(int id, UpdateInstructorProfileDto dto);
        Task<bool> DeleteAsync(int id);
    }
}