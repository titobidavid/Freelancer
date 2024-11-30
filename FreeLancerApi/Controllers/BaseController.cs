using Microsoft.AspNetCore.Mvc;
using ResponseModelTitobi;

namespace FreeLancer.Api.Controllers
{
	[ApiController]
	public class BaseController : ControllerBase
	{
		public IActionResult ReturnValue<T>(ResponseModel<T> response)
		{
			int statusCode = 0;
			if (response.IsSuccessful == true)
			{
				return Ok(response.data);
			}
			else
			{
				switch (response.ErrorCodes)
				{
					case ErrorCodes.Failed:
						statusCode = StatusCodes.Status400BadRequest;
						break;
					case ErrorCodes.UnAuthorized:
						statusCode = StatusCodes.Status401Unauthorized;
						break;
					case ErrorCodes.TokenExpired:
						break;
					case ErrorCodes.ServerError:
						statusCode = StatusCodes.Status500InternalServerError;
						break;
					case ErrorCodes.DataNotFound:
						statusCode = StatusCodes.Status404NotFound;
						break;
					default:
						break;
				}

				if(statusCode > 0)
				{
					return StatusCode(statusCode, response);
				}
				else
				{
					return BadRequest();
				}
				
			}
		}
	}
}
