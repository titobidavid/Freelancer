using FreeLancer.Models.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeLancer.Models.Models.DTO
{
    public class JobDTO
    {
		public int Id { get; set; }
		public required string Description { get; set; }
		public required string SkillLevel { get; set; }
		public string SkillName { get; set; }
		public decimal PaymentAmount { get; set; }
		public required string Duration { get; set; }
	}

}
