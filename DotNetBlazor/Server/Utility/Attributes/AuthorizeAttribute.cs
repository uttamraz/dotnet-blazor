namespace DotNetBlazor.Server.Helpers.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute
    { }
    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    //public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    //{
    //    public void OnAuthorization(AuthorizationFilterContext context)
    //    {
    //        // skip authorization if action is decorated with [AllowAnonymous] attribute
    //        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
    //        if (allowAnonymous)
    //            return;

    //        // authorization
    //        string auth = context.HttpContext.Request.Headers["Authorization"];
    //        if (string.IsNullOrEmpty(auth))
    //        {
    //            context.Result = new JsonResult(new CommonResponse(null, new Response { Status = StatusCodes.Status401Unauthorized, Message = HttpStatusCode.Unauthorized.ToString() }));
    //        }
    //    }
    //}
}
