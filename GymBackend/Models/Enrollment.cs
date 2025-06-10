using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_api.Model
{
    public class Enrollment
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public double Rating { get; set; }
        public required string Hour { get; set; }

        public virtual ICollection<GymMember>? GymMembers { get; set; }
    }
}