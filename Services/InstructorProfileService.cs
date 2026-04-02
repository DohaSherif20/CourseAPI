using CourseAPI.Data;
using CourseAPI.DTOs.InstructorProfiles;
using CourseAPI.Interfaces;
using CourseAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseAPI.Services
{
    public class InstructorProfileService : IInstructorProfileService
    {
        private readonly AppDbContext _context;

        public InstructorProfileService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InstructorProfileReadDto>> GetAllAsync()
        {
            return await _context.InstructorProfiles
                .AsNoTracking()
                .Include(p => p.User)
                .Select(p => new InstructorProfileReadDto
                {
                    Id = p.Id,
                    Bio = p.Bio,
                    UserId = p.UserId,
                    UserName = p.User.Name
                })
                .ToListAsync();
        }

        public async Task<InstructorProfileReadDto?> GetByIdAsync(int id)
        {
            return await _context.InstructorProfiles
                .AsNoTracking()
                .Include(p => p.User)
                .Where(p => p.Id == id)
                .Select(p => new InstructorProfileReadDto
                {
                    Id = p.Id,
                    Bio = p.Bio,
                    UserId = p.UserId,
                    UserName = p.User.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task<InstructorProfileReadDto?> CreateAsync(CreateInstructorProfileDto dto)
        {
            var userExists = await _context.Users
                .AnyAsync(u => u.Id == dto.UserId && u.Role == "Instructor");

            if (!userExists)
                return null;

            var alreadyHasProfile = await _context.InstructorProfiles
                .AnyAsync(p => p.UserId == dto.UserId);

            if (alreadyHasProfile)
                return null;

            var profile = new InstructorProfile
            {
                Bio = dto.Bio,
                UserId = dto.UserId
            };

            _context.InstructorProfiles.Add(profile);
            await _context.SaveChangesAsync();

            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == dto.UserId);

            return new InstructorProfileReadDto
            {
                Id = profile.Id,
                Bio = profile.Bio,
                UserId = profile.UserId,
                UserName = user?.Name ?? ""
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateInstructorProfileDto dto)
        {
            var profile = await _context.InstructorProfiles.FirstOrDefaultAsync(p => p.Id == id);
            if (profile == null)
                return false;

            profile.Bio = dto.Bio;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var profile = await _context.InstructorProfiles.FirstOrDefaultAsync(p => p.Id == id);
            if (profile == null)
                return false;

            _context.InstructorProfiles.Remove(profile);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}