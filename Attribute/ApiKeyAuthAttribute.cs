using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace lizi_mail_api.Middleware
{
    public class ApiKeyAuthAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("X-API-KEY", out var apiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (apiKey != "")
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
