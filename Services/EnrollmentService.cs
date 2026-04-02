using CourseAPI.Data;
using CourseAPI.DTOs.Enrollments;
using CourseAPI.Interfaces;
using CourseAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseAPI.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly AppDbContext _context;

        public EnrollmentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EnrollmentReadDto>> GetAllAsync()
        {
            return await _context.Enrollments
                .AsNoTracking()
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Select(e => new EnrollmentReadDto
                {
                    Id = e.Id,
                    StudentId = e.StudentId,
                    StudentName = e.Student.Name,
                    CourseId = e.CourseId,
                    CourseTitle = e.Course.Title
                })
                .ToListAsync();
        }

        public async Task<EnrollmentReadDto?> GetByIdAsync(int id)
        {
            return await _context.Enrollments
                .AsNoTracking()
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Where(e => e.Id == id)
                .Select(e => new EnrollmentReadDto
                {
                    Id = e.Id,
                    StudentId = e.StudentId,
                    StudentName = e.Student.Name,
                    CourseId = e.CourseId,
                    CourseTitle = e.Course.Title
                })
                .FirstOrDefaultAsync();
        }

        public async Task<EnrollmentReadDto?> CreateAsync(CreateEnrollmentDto dto)
        {
            var studentExists = await _context.Users
                .AnyAsync(u => u.Id == dto.StudentId && u.Role == "Student");

            var courseExists = await _context.Courses
                .AnyAsync(c => c.Id == dto.CourseId);

            if (!studentExists || !courseExists)
                return null;

            var duplicate = await _context.Enrollments
                .AnyAsync(e => e.StudentId == dto.StudentId && e.CourseId == dto.CourseId);

            if (duplicate)
                return null;

            var enrollment = new Enrollment
            {
                StudentId = dto.StudentId,
                CourseId = dto.CourseId
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            var student = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == dto.StudentId);
            var course = await _context.Courses.AsNoTracking().FirstOrDefaultAsync(c => c.Id == dto.CourseId);

            return new EnrollmentReadDto
            {
                Id = enrollment.Id,
                StudentId = enrollment.StudentId,
                StudentName = student?.Name ?? "",
                CourseId = enrollment.CourseId,
                CourseTitle = course?.Title ?? ""
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var enrollment = await _context.Enrollments.FirstOrDefaultAsync(e => e.Id == id);
            if (enrollment == null)
                return false;

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}