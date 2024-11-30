using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeLancer.Models.Models.Domain
{
    public class JobApplication
    {
        public int Id { get; set; }
        public int JobId { get; set; }  

        public int FreelancerID { get; set; }

        public string? Application { get; set; }
        public bool IsApplied { get; set; } = false;
        public DateTime TimeCreated { get; set; } 
        public DateTime TimeUpdated { get; set; }
    }
}
