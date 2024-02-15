using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace OnlineStore.WebApi.Filters.Errors
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute, IFilterMetadata
    {
        public override void OnException(ExceptionContext context)
        {

            //Log.Error(context.Exception, "An error occurred in the route {0}", context.Exception.Source);

            context.HttpContext.Response.StatusCode = (Int32)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult("Internal Server Error");

            context.ExceptionHandled = true;

        }
    }
}
