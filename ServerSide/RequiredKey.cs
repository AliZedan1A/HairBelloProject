using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ServerSide
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequiredKey : Attribute, IAsyncActionFilter
    {
        public RequiredKey()
        {
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            if (context.HttpContext?.Request.Headers.TryGetValue("VLToken", out var z) == true)
            {
                if (z == "")
                {
                    await next();
                    return;
                }
                else
                {
                    context.Result = new UnauthorizedResult();
                    return;

                }
            }
            else
            {

                context.Result = new UnauthorizedResult();
                return;

            }


            

          
        }

    }
}
