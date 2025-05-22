using System.ComponentModel.DataAnnotations;

namespace Gym_api.DTO
{
    public class CreateEnrollmentDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? PlanFilePath { get; set; }
    }
}
