using CourseAPI.DTOs.InstructorProfiles;
using CourseAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InstructorProfilesController : ControllerBase
    {
        private readonly IInstructorProfileService _profileService;

        public InstructorProfilesController(IInstructorProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var profiles = await _profileService.GetAllAsync();
            return Ok(profiles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var profile = await _profileService.GetByIdAsync(id);

            if (profile == null)
                return NotFound(new { message = "Instructor profile not found." });

            return Ok(profile);
        }

        [HttpPost]
        [Authorize(Roles = "Instructor,Admin")]
        public async Task<IActionResult> Create(CreateInstructorProfileDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdProfile = await _profileService.CreateAsync(dto);

            if (createdProfile == null)
                return BadRequest(new { message = "Instructor not found or profile already exists." });

            return Ok(createdProfile);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Instructor,Admin")]
        public async Task<IActionResult> Update(int id, UpdateInstructorProfileDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _profileService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound(new { message = "Instructor profile not found." });

            return Ok(new { message = "Instructor profile updated successfully." });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _profileService.DeleteAsync(id);

            if (!deleted)
                return NotFound(new { message = "Instructor profile not found." });

            return Ok(new { message = "Instructor profile deleted successfully." });
        }
    }
}