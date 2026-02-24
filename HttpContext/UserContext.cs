using System.Security.Claims;

namespace lizi_mail_api.HttpContext
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContext;

        public UserContext(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public Guid? UserId
        {
            get
            {
                var id = _httpContext.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return id != null ? Guid.Parse(id) : null;
            }
        }

        public string? Email =>
            _httpContext.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
    }
}
