namespace Gym_api.DTO
{
    public class GymMemberDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MembershipType { get; set; }
        public int EnrollmentId { get; set; }
        public string? PlanFilePath { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
