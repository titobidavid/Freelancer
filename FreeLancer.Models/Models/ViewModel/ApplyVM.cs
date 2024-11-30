using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeLancer.Models.Models.ViewModel
{
    public class ApplyVM
    {
        public int JobID { get; set; }
        public string JobApplication { get; set; }

        public int FreelancerID { get; set; }
        public bool IsApplied { get; set; } = false;


    }
}
