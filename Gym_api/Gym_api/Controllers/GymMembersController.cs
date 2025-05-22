using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gym_api.Model;
using Gym_api.DTO;
using System.IO;

namespace Gym_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymMembersController : ControllerBase
    {
        private readonly GymDbContext _context;

        public GymMembersController(GymDbContext context)
        {
            _context = context;
        }

        // GET: api/GymMembers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GymMemberDTO>>> GetGymMembers()
        {
            try
            {
                var members = await _context.GymMembers.ToListAsync();
                var dtoList = members.Select(m => new GymMemberDTO
                {
                    Id = m.Id,
                    Name = m.Name,
                    Surname = m.Surname,
                    MembershipType = m.MembershipType,
                    EnrollmentId = m.EnrollmentId,
                    PlanFilePath = m.PlanFilePath,
                    JoinDate = m.JoinDate
                }).ToList();

                return Ok(dtoList);
            }
            catch
            {
                return StatusCode(500, "An error occurred while fetching GymMembers.");
            }
        }

        // GET: api/GymMembers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GymMemberDTO>> GetGymMember(int id)
        {
            try
            {
                var m = await _context.GymMembers.FindAsync(id);
                if (m == null) return NotFound();

                return new GymMemberDTO
                {
                    Id = m.Id,
                    Name = m.Name,
                    Surname = m.Surname,
                    MembershipType = m.MembershipType,
                    EnrollmentId = m.EnrollmentId,
                    PlanFilePath = m.PlanFilePath,
                    JoinDate = m.JoinDate
                };
            }
            catch
            {
                return StatusCode(500, "An error occurred while fetching the GymMember.");
            }
        }

        // POST: api/GymMembers
        [HttpPost]
        public async Task<ActionResult<GymMemberDTO>> PostGymMember(CreateGymMemberDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newMember = new GymMember
            {
                Name = dto.Name,
                Surname = dto.Surname,
                MembershipType = dto.MembershipType,
                EnrollmentId = dto.EnrollmentId,
                JoinDate = DateTime.UtcNow
            };

            try
            {
                _context.GymMembers.Add(newMember);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetGymMember", new { id = newMember.Id }, new GymMemberDTO
                {
                    Id = newMember.Id,
                    Name = newMember.Name,
                    Surname = newMember.Surname,
                    MembershipType = newMember.MembershipType,
                    EnrollmentId = newMember.EnrollmentId,
                    JoinDate = newMember.JoinDate,
                    PlanFilePath = newMember.PlanFilePath
                });
            }
            catch
            {
                return StatusCode(500, "An error occurred while adding the GymMember.");
            }
        }

        // PUT: api/GymMembers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGymMember(int id, UpdateGymMemberDTO dto)
        {
            try
            {
                var member = await _context.GymMembers.FindAsync(id);
                if (member == null)
                    return NotFound($"GymMember with ID {id} not found");

                member.Name = dto.Name;
                member.Surname = dto.Surname;
                member.MembershipType = dto.MembershipType;
                member.EnrollmentId = dto.EnrollmentId;

                await _context.SaveChangesAsync();

                return Ok(new GymMemberDTO
                {
                    Id = member.Id,
                    Name = member.Name,
                    Surname = member.Surname,
                    MembershipType = member.MembershipType,
                    EnrollmentId = member.EnrollmentId,
                    JoinDate = member.JoinDate,
                    PlanFilePath = member.PlanFilePath
                });
            }
            catch
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        // DELETE: api/GymMembers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGymMember(int id)
        {
            try
            {
                var gymMember = await _context.GymMembers.FindAsync(id);
                if (gymMember == null)
                    return NotFound();

                _context.GymMembers.Remove(gymMember);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch
            {
                return StatusCode(500, "An error occurred while deleting the GymMember.");
            }
        }

        // POST: api/GymMembers/{id}/upload-plan
        [HttpPost("{id}/upload-plan")]
        public async Task<IActionResult> UploadPlan(int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file provided.");

            var allowedExtensions = new[] { ".pdf", ".doc", ".docx", ".jpg", ".png", ".zip", ".txt" };
            var ext = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(ext))
                return BadRequest("File type not allowed.");

            var member = await _context.GymMembers.FindAsync(id);
            if (member == null)
                return NotFound("GymMember not found.");

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine("wwwroot/uploads", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            member.PlanFilePath = $"/uploads/{fileName}";
            await _context.SaveChangesAsync();

            return Ok(new { path = member.PlanFilePath });
        }
    }
}
