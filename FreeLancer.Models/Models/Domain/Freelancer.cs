using FreeLancer.Models.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeLancer.Models.Models.Domain
{
    public class Freelancer:Auth
    {
        public int SkillId { get; set; }
        public Role role { get { return Role.Freelancer; } }
        public Experience SkillLevel { get; set; } = Experience.Beginner;
    }
}
