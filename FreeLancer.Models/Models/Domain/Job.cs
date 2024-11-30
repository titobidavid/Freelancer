using FreeLancer.Models.Models;
using FreeLancer.Models.Models.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FreeLancer.Models.Models.Domain
{
    public class Job
    {
        public int Id { get; set; }
        public required string Description { get; set; }

        [JsonIgnore]
        public required Experience SkillLevel { get; set; }
        public required int SkillId { get; set; }
        public decimal PaymentAmount { get; set; }
        public required string Duration { get; set; }
        [ForeignKey("HirerId")]
        public int Job_Hirer_Id { get; set; }

        [ForeignKey("FreeLancerId")]
        public int FreeLancer_Id { get; set; }

        public IEnumerable<JobApplication> jobApplications { get; set; }
        public bool IsApplied { get; set; } = false;


    }
}
