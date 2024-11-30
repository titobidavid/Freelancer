using FreeLancer.Models.Models.Domain;
using FreeLancer.Models.Models.DTO;
using ResponseModelTitobi;
using System.Threading.Tasks;

namespace FreeLancer.Services.Jobs
{
    public interface IJobService
	{
		public Task<ResponseModel<Job>> CreateJob(JobDTO model);
		public Task<ResponseModel<Job>> UpdateJob(JobDTO model, int Id);
		public Task<ResponseModel<Job>> DeleteJob(int jobId);
		public Task<ResponseModel<JobDTO>> GetJobById(int jobId);
        public Task<ResponseModel<JobDTO>> GetJobById(string jobId);

        public Task<ResponseModel<IEnumerable<JobDTO>>> GetJobs();

	}
}
