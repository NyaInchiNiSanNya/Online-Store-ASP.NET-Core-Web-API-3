using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OnlineStore.BusinessLogic.Excpetions;

namespace OnlineStore.WebApi.Filters.Errors
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case ObjectNotFoundException:
                        context.Response.StatusCode = 404;
                        break;
                    case BadAuthorizeException:
                        context.Response.StatusCode = 400;
                        break;
                    case InvalidIdException:
                        context.Response.StatusCode = 400;
                        break;
                    case BadRegistrationException:
                        context.Response.StatusCode = 400;
                        break;
                    case ObjectAlreadyExistException:
                        context.Response.StatusCode = 400;
                        break;
                    default:
                        context.Response.StatusCode = 500;
                        break;
                }

                context.Response.ContentType = "application/json";

                var errorResponse = new ErrorResponse()
                {
                    StatusCode = context.Response.StatusCode,
                    ErrorMessage =  ex.Message
                };

                var jsonResponse = JsonConvert.SerializeObject(errorResponse);
                
                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }

}
