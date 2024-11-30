using FreeLancer.Models.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ResponseModelTitobi;

namespace FreeLancer.Api.Filters
{
    public class VerifyNullJob : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            ResponseModel<JobApplication> responseModel = new();
            if (context.ModelState.Count == 0)
            {
                responseModel.IsSuccessful = false;
                responseModel.Message = "Null value";
                responseModel.ErrorCodes = ErrorCodes.ServerError;
                context.Result = new ObjectResult(responseModel);
            }
        }

       
    }
}
