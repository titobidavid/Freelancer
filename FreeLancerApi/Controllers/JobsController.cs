using FreeLancer.Api.Filters;
using FreeLancer.Data;
using FreeLancer.Models.Models.Domain;
using FreeLancer.Models.Models.DTO;
using FreeLancer.Models.Models.Enums;
using FreeLancer.Services.Jobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FreeLancer.Api.Controllers
{
	[Authorize]
    [Route("api/[controller]/[action]")]
	[ApiController]
	public class JobsController : BaseController
	{
		public readonly IJobService _job;
		public readonly AppDbContext _database;
        public JobsController(IJobService job, AppDbContext database)
        {
            _job = job;
            _database = database;
        }
        [HttpGet("item/{format?}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[FormatFilter]
		[Produces("application/xml", "application/json")]
        public async Task<IActionResult> GetAll()
		{
			var result = await _job.GetJobs();
			return ReturnValue(result);	
		}
        [HttpGet("get_byId/{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] int id)
		{
			var result = await _job.GetJobById(id);
			return ReturnValue(result);
		}

        [HttpGet]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[Route("get_byName/{jobName}")]
        public async Task<IActionResult> GetById([FromRoute] string jobName)
        {
            var result = await _job.GetJobById(jobName);
            return ReturnValue(result);
        }
        [HttpPost("createJob")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[CheckUserRole]
        public async Task<IActionResult> Post([FromBody] JobDTO model)
		{
			var result = await _job.CreateJob(model);
			return ReturnValue(result);
		}

		[HttpPut("updateJob/{id}")]
		public async Task<IActionResult> Put([FromBody] JobDTO model, [FromRoute] int id)
		{
			var result = await _job.UpdateJob(model, id);
			return ReturnValue(result);
		}

		[HttpDelete("DeleteJob/{id}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var result = await _job.DeleteJob(id);
			return ReturnValue(result);
		}
	}
}
