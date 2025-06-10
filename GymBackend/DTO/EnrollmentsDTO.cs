namespace Gym_api.DTO
{
    public class EnrollmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? PlanFilePath { get; set; }
        public double Rating { get; set; }
        public string Hour { get; set; } = string.Empty;

        public List<GymMemberDTO> GymMembers { get; set; } = new();
    }
}
