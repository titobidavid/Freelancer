using FreeLancer.Models.Models.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ResponseModelTitobi;

namespace FreeLancer.Api.Filters
{
    public class CheckUserRole : Attribute, IActionFilter
    { 
        public void OnActionExecuted(ActionExecutedContext context)
        {
          
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var role = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;
            if ((Role)Enum.Parse(typeof(Role), role) == Role.Freelancer)
            {
                var response = new ResponseModel<CheckUserRole>
                {
                    ErrorCodes = ErrorCodes.UnAuthorized,
                    IsSuccessful = false,
                    Message = "You are not authorized to create a job register as a Hirer to do this"
                };
                context.Result = new ObjectResult(response);
            }
        }
    }
}
