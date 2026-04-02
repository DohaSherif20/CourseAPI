using CourseAPI.Data;
using CourseAPI.DTOs.Courses;
using CourseAPI.Interfaces;
using CourseAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseAPI.Services
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext _context;

        public CourseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CourseReadDto>> GetAllAsync()
        {
            return await _context.Courses
                .AsNoTracking()
                .Include(c => c.Instructor)
                .Select(c => new CourseReadDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    InstructorId = c.InstructorId,
                    InstructorName = c.Instructor.Name
                })
                .ToListAsync();
        }

        public async Task<CourseReadDto?> GetByIdAsync(int id)
        {
            return await _context.Courses
                .AsNoTracking()
                .Include(c => c.Instructor)
                .Where(c => c.Id == id)
                .Select(c => new CourseReadDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    InstructorId = c.InstructorId,
                    InstructorName = c.Instructor.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task<CourseReadDto?> CreateAsync(CreateCourseDto dto)
        {
            var instructorExists = await _context.Users
                .AnyAsync(u => u.Id == dto.InstructorId && u.Role == "Instructor");

            if (!instructorExists)
                return null;

            var course = new Course
            {
                Title = dto.Title,
                Description = dto.Description,
                InstructorId = dto.InstructorId
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            var instructor = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == dto.InstructorId);

            return new CourseReadDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                InstructorId = course.InstructorId,
                InstructorName = instructor?.Name ?? ""
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateCourseDto dto)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);
            if (course == null)
                return false;

            var instructorExists = await _context.Users
                .AnyAsync(u => u.Id == dto.InstructorId && u.Role == "Instructor");

            if (!instructorExists)
                return false;

            course.Title = dto.Title;
            course.Description = dto.Description;
            course.InstructorId = dto.InstructorId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);
            if (course == null)
                return false;

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}