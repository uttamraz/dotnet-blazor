using DotNetBlazor.Server.Utility.Helpers;
using DotNetBlazor.Shared.Models.Common;
using System.Net.Mime;
using System.Net;
using System.Text.Json;
using DotNetBlazor.Server.Helpers.Attributes;
using Microsoft.Extensions.Caching.Memory;

namespace DotNetBlazor.Server.Utility.Middleware
{
    public class AuthorizeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;
        private readonly IContextHelper _contextHelper;
        private readonly IJwtUtils _jwtUtils;

        public AuthorizeMiddleware(RequestDelegate next, IMemoryCache cache, IContextHelper contextHelper, IJwtUtils jwtUtils)
        {
            _next = next;
            _cache = cache;
            _contextHelper = contextHelper;
            _jwtUtils = jwtUtils;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.GetEndpoint()?.Metadata?.GetMetadata<AllowAnonymousAttribute>() == null)
            {
                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var message = JsonSerializer.Serialize(new CommonResponse(null, StatusCodes.Status401Unauthorized, HttpStatusCode.Unauthorized.ToString()), options);
                var authorizationId = _contextHelper.Token();
                if (string.IsNullOrEmpty(authorizationId))
                {
                    await SendResponse(context, StatusCodes.Status401Unauthorized, message);
                    return;
                }

                var id = _jwtUtils.ValidateJwtToken(authorizationId);
                if (string.IsNullOrEmpty(id))
                {
                    await SendResponse(context, StatusCodes.Status401Unauthorized, message);
                    return;
                }
                var user = await _cache.GetRecordAsync<UserContext>(authorizationId);
                if (user == null)
                {
                    await SendResponse(context, StatusCodes.Status401Unauthorized, message);
                    return;
                }
                if (!context.Request.Headers.ContainsKey("Id"))
                    context.Request.Headers.Add("Id", user.Id.ToString());
                if (!context.Request.Headers.ContainsKey("Token"))
                    context.Request.Headers.Add("Token", authorizationId);
                if (!context.Request.Headers.ContainsKey("Email") && !string.IsNullOrEmpty(user.Email))
                    context.Request.Headers.Add("Email", user.Email);
            }
            await _next(context);
        }

        private static async Task SendResponse(HttpContext context, int status, string message)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = status;
            await context.Response.WriteAsync(message);
            return;
        }
    }
}
