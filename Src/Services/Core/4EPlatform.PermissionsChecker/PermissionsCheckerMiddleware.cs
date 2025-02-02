using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace _4EPlatform.PermissionsChecker
{
    public class PermissionsCheckerMiddleware : IMiddleware
    {
        private readonly PermissionService.PermissionServiceClient _permissionServiceClient;

        public PermissionsCheckerMiddleware(PermissionService.PermissionServiceClient permissionServiceClient)
        {
            _permissionServiceClient = permissionServiceClient;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Protocol == "HTTP/2")
            {
                await next.Invoke(context);
                return;
            }
            string? url = context.Request.Path.Value.ToString();
            var ActionName = context.Request.RouteValues["action"].ToString();
            var ControllerName = context.Request.RouteValues["controller"].ToString();

            if (
                (ControllerName == "Account" && ActionName == "Login")
                || (ControllerName == "Domain" && ActionName == "GeAllDomains")
                || url.Contains("swagger")
                || url.Contains(".well-known/openid-configuration")
                || url.Contains("connect/token")
                )
            {
                await next.Invoke(context);
                return;
            }
            string verb = context.Request.Method.ToString();

            string UserId = context.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var responseGrpc = await _permissionServiceClient.GetUserPermissionAsync(new ReadUserIdRequest { UserId = UserId });
            if ((bool)responseGrpc.IsSystemAdmin)
            {
                await next.Invoke(context);
                return;
            }
            if (responseGrpc.Permission.Any())
            {
                bool hasPermission = responseGrpc.Permission.Any(x => x.MethodVerd == verb && ControllerName == x.ControllerName && ActionName == x.ActionName);

                if (hasPermission)
                {
                    await next.Invoke(context);
                }
                else
                {
                    context.Response.StatusCode = 403;
                    // Set response body
                    await context.Response.WriteAsync("Unauthorized. You are not authorized to access this resource.");
                }
            }
            else
            {
                context.Response.StatusCode = 403;
                // Set response body
                await context.Response.WriteAsync("Unauthorized. You are not authorized to access this resource.");
            }
        }
    }
}