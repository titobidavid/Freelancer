using FreeLancer.Models.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeLancer.Models.Models.Domain
{
    public class Hirer:Auth
    {
        public List<Job> jobs { get; set; }

        public string CompanyName { get; set; }

        public Role role {get{return Role.Hirer;}}
    }
}
