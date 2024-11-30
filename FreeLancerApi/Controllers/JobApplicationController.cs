using FreeLancer.Models.Models.Domain;
using FreeLancer.Models.Models.ViewModel;
using FreeLancer.Services.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeLancer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobApplicationController : BaseController
    {
        public readonly IApplication _application;
        public JobApplicationController(IApplication application)
        {
            _application = application;
        }

        [HttpPost("createApplication")]
        public async Task<IActionResult> CreateApplication(ApplyVM model)
        {
            var result = await _application.CreateApplication(model);
            return ReturnValue(result);
        }
        [HttpPut("acceptApplication/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AcceptApplication(ApplyVM model, int Id)
        {
            var result = await _application.AcceptApplication(model, Id);
            return ReturnValue<JobApplication>(result);
        }

        [HttpGet("GetApplicants/{format?}")]
        [Produces("application/xml", "application/json")]
        [FormatFilter]
        public IActionResult GetApplicants()
        {
            var result =  _application.ListApplicant();
            return ReturnValue(result);
        }


    }
}
