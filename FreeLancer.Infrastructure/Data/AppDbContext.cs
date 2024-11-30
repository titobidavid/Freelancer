using FreeLancer.Models.Models.Domain;
using FreeLancer.Models.Models.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FreeLancer.Data
{
    public class AppDbContext : IdentityDbContext
	{
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options) { }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Freelancer> freelancers { get; set; }
        public DbSet<Auth> user { get; set; }

        public DbSet<JobApplication> JobApplications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>().HasData(
                        new Job
                        {
                            Id = 1,
                            PaymentAmount = 2500,
                            Description = "Create a website for an existing e-commerce app",
                            Duration = "2 months",
                            SkillLevel = Experience.Beginner,
                            SkillId = 1
                        },
                        new Job
                        {
                            Id = 2,
                            PaymentAmount = 100500,
                            Description = "Design the landing page for a website on figma",
                            Duration = "2 months",
                            SkillLevel = Experience.Beginner,
                            SkillId = 2
                        }
                );
            modelBuilder.Entity<Skill>().HasData(
                     new Skill
                     {
                         Id = 1,
                         SkillName = "Programmer"
                     },
                     new Skill
                     {
                         Id = 2,
                         SkillName = "Graphics Designer"
                     }
                );
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Job>().Property(t => t.SkillLevel)
               .HasConversion(
               v => v.ToString(),
               v => (Experience)Enum.Parse(typeof(Experience), v));
        }


    }
}
