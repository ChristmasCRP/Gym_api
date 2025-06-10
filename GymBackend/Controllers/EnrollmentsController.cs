using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gym_api.Model;
using Gym_api.DTO;

namespace Gym_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly GymDbContext _context;

        public EnrollmentsController(GymDbContext context)
        {
            _context = context;
        }

        // GET: api/Enrollments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnrollmentDTO>>> GetEnrollments()
        {
            var enrollments = await _context.Enrollments
                .Include(e => e.GymMembers)
                .ToListAsync();

            var dtoList = enrollments.Select(e => new EnrollmentDTO
            {
                Id = e.Id,
                Name = e.Name,
                Hour = e.Hour,
                Rating = e.Rating,
                PlanFilePath = null,
                GymMembers = e.GymMembers?.Select(m => new GymMemberDTO
                {
                    Id = m.Id,
                    Name = m.Name,
                    Surname = m.Surname,
                    MembershipType = m.MembershipType,
                    EnrollmentId = m.EnrollmentId,
                    PlanFilePath = m.PlanFilePath,
                    JoinDate = m.JoinDate
                }).ToList() ?? new()
            }).ToList();

            return Ok(dtoList);
        }

        // GET: api/Enrollments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EnrollmentDTO>> GetEnrollment(int id)
        {
            var e = await _context.Enrollments
                .Include(e => e.GymMembers)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (e == null)
                return NotFound();

            var dto = new EnrollmentDTO
            {
                Id = e.Id,
                Name = e.Name,
                Hour = e.Hour,
                Rating = e.Rating,
                PlanFilePath = null,
                GymMembers = e.GymMembers?.Select(m => new GymMemberDTO
                {
                    Id = m.Id,
                    Name = m.Name,
                    Surname = m.Surname,
                    MembershipType = m.MembershipType,
                    EnrollmentId = m.EnrollmentId,
                    PlanFilePath = m.PlanFilePath,
                    JoinDate = m.JoinDate
                }).ToList() ?? new()
            };

            return Ok(dto);
        }

        // POST: api/Enrollments
        [HttpPost]
        public async Task<ActionResult<EnrollmentDTO>> PostEnrollment(CreateEnrollmentDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var enrollment = new Enrollment
            {
                Name = dto.Name,
                Hour = "00:00",
                Rating = 3.0,
                GymMembers = new List<GymMember>()
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEnrollment", new { id = enrollment.Id }, new EnrollmentDTO
            {
                Id = enrollment.Id,
                Name = enrollment.Name,
                Hour = enrollment.Hour,
                Rating = enrollment.Rating,
                PlanFilePath = null,
                GymMembers = new()
            });
        }

        // DELETE: api/Enrollments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            try
            {
                var enrollment = await _context.Enrollments
                    .Include(e => e.GymMembers)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (enrollment == null)
                    return NotFound($"Enrollment with ID {id} not found.");

                _context.GymMembers.RemoveRange(enrollment.GymMembers);
                _context.Enrollments.Remove(enrollment);

                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                var finalException = ex;
                while (finalException.InnerException != null)
                    finalException = finalException.InnerException;

                return new ObjectResult(finalException.Message) { StatusCode = 500 };
            }
        }
    }
}
