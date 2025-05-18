using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_api.Model
{
    public class GymMember
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(30, MinimumLength =2, ErrorMessage = "Name must be betweend 2 and 30 char")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Surname must be betweend 2 and 30 char")]
        public string Surname { get; set; }
        public required string MembershipType { get; set; }
        public DateTime JoinDate { get; set; }

        [ForeignKey("Enrollment")]
        public int EnrollmentId { get; set; }
    }
}
