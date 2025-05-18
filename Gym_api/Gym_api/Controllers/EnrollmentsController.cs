using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gym_api.Model;

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
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetEnrollments()
        {
            var enrollments = await _context.Enrollments
                .Include(e => e.GymMembers)
                .ToListAsync();
            return Ok(enrollments);
        }

        // GET: api/Enrollments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Enrollment>> GetEnrollment(int id)
        {
            var enrollment = await _context.Enrollments
                .Include(e => e.GymMembers)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (enrollment == null)
            {
                return NotFound();
            }

            return Ok(enrollment);
        }

        // POST: api/Enrollments
        [HttpPost]
        public async Task<ActionResult<Enrollment>> PostEnrollment(Enrollment enrollment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEnrollment", new { id = enrollment.Id }, enrollment);
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
            }
            catch (Exception ex)
            {
                var finalException = ex;
                while (finalException.InnerException != null)
                {
                    finalException = ex.InnerException;
                }
                return new ObjectResult(finalException.Message) { StatusCode = 500 };
            }

            return Ok();
        }
    }
}
