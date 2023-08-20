namespace DotNetBlazor.Server.Utility.Helpers
{

    public interface IContextHelper
    {
        public int Id();
        public string? Token();
        public string? Username();
        public string? Email();
        public string? DebugId();
        public IHeaderDictionary? Headers();
    }

    public class ContextHelper : IContextHelper
    {
        private readonly IHttpContextAccessor context;

        public ContextHelper(IHttpContextAccessor httpContextAccessor)
        {
            context = httpContextAccessor;
        }
        public int Id()
        {
            return Convert.ToInt32(context?.HttpContext?.Request.Headers["Id"]);
        }
        public string? Token()
        {
            string? auth = context?.HttpContext?.Request.Headers["Authorization"];
            if (auth != null && auth.StartsWith("Bearer"))
            {
                return auth.Substring("Bearer ".Length).Trim();
            }
            return null;
        }
        public string? Username()
        {
            return context?.HttpContext?.Request.Headers["Username"];
        }

        public string? Email()
        {
            return context?.HttpContext?.Request.Headers["Email"];
        }
        public string? DebugId()
        {
            return context?.HttpContext?.Request.Headers["DebugId"];
        }

        public IHeaderDictionary? Headers()
        {
            return context?.HttpContext?.Request.Headers;
        }
    }
}
