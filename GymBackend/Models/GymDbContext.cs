using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;

namespace Gym_api.Model
{
    public class GymDbContext : DbContext
    {
        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options) { }

        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<GymMember> GymMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment { Id = 1133, Name = "Yoga", Hour = "Monday 15:30", Rating = 3.7 },
                new Enrollment { Id = 1088, Name = "Pilates", Hour = "Thuesday 16:30", Rating = 2.7 },
                new Enrollment { Id = 1153, Name = "Crossfit", Hour = "Friday 18:30", Rating = 5.0 },
                new Enrollment { Id = 1232, Name = "Stretching", Hour = "Saturday 10:00", Rating = 4.8 }
            );

            modelBuilder.Entity<GymMember>().HasData(
                new GymMember { Id = 1, Name = "John", Surname = "Deer", MembershipType = "Standard", JoinDate = new DateTime(2023, 1, 1), EnrollmentId = 1133 },
                new GymMember { Id = 2, Name = "Kamil", Surname = "Kowalski", MembershipType = "Standard", JoinDate = new DateTime(2024, 5, 7), EnrollmentId = 1088 },
                new GymMember { Id = 3, Name = "Piotr", Surname = "Stanowski", MembershipType = "Premium", JoinDate = new DateTime(2025, 1, 2), EnrollmentId = 1133 },
                new GymMember { Id = 4, Name = "Jakub", Surname = "Stanley", MembershipType = "Premium", JoinDate = new DateTime(2024, 7, 11), EnrollmentId = 1153 },
                new GymMember { Id = 5, Name = "Elon", Surname = "Musk", MembershipType = "Premium", JoinDate = new DateTime(2023, 9, 10), EnrollmentId = 1232 },
                new GymMember { Id = 6, Name = "Kornelia", Surname = "Casowsky", MembershipType = "Standard", JoinDate = new DateTime(2024, 11, 23), EnrollmentId = 1153 },
                new GymMember { Id = 7, Name = "Martin", Surname = "Stankiewicz", MembershipType = "Plus", JoinDate = new DateTime(2023, 10, 10), EnrollmentId = 1153 }
            );
        }
    }

    public class GymDbContextFactory : IDesignTimeDbContextFactory<GymDbContext>
    {
        public GymDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<GymDbContext>();
            var connectionString = configuration.GetConnectionString("GymDatBas");
            optionsBuilder.UseSqlServer(connectionString);

            return new GymDbContext(optionsBuilder.Options);
        }
    }
}
