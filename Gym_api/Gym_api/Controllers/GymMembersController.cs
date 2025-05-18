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
    public class GymMembersController : ControllerBase
    {
        private readonly GymDbContext _context;

        public GymMembersController(GymDbContext context)
        {
            _context = context;
        }


        /// /////////////////////////////////
      


        // GET: api/GymMembers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GymMember>>> GetGymMembers()
        {
            try
            {
                return await _context.GymMembers.ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching GymMembers.");
            }
        }




        // GET: api/GymMembers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GymMember>> GetGymMember(int id)
        {
            try
            {
                var gymMember = await _context.GymMembers.FindAsync(id);

                if (gymMember == null)
                {
                    return NotFound();
                }

                return gymMember;
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the GymMember.");
            }
        }





        // POST: api/GymMembers
        [HttpPost]
        public async Task<ActionResult<GymMember>> PostGymMember(GymMember gymMember)
        {
            try
            {
                _context.GymMembers.Add(gymMember);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetGymMember", new { id = gymMember.Id }, gymMember);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the GymMember.");
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
                {
                    return NotFound();
                }

                _context.GymMembers.Remove(gymMember);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the GymMember.");
            }
        }





        // UPDATE: api/GymMembers
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGymMember(int id, GymMember updatedMember)
        {
            try
            {
                var GymMember2 = await _context.GymMembers.FindAsync(id);
                if (GymMember2 == null)
                {
                    return NotFound($"GymMember with ID {id} not found");
                }

                GymMember2.Name = updatedMember.Name;
                GymMember2.Surname = updatedMember.Surname;
                GymMember2.MembershipType = updatedMember.MembershipType;
                GymMember2.EnrollmentId = updatedMember.EnrollmentId;

                await _context.SaveChangesAsync();
                return Ok(GymMember2);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Error during saving to DataBase");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
