using System.ComponentModel.DataAnnotations;

namespace Gym_api.DTO
{
    public class CreateGymMemberDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Surname { get; set; }

        [Required]
        public string MembershipType { get; set; }

        [Required]
        public int EnrollmentId { get; set; }
    }
}
