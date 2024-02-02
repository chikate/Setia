using AutoMapper;
using Setia.Data;
using Setia.Services.Interfaces;
using System.Security.Claims;

namespace Setia.Services
{
    public class AuthenticationService : IAuth
    {
        private readonly SetiaContext _context;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IMapper _mapper;

        public AuthenticationService
        (
            SetiaContext context,
            ILogger<AuthenticationService> logger,
            IMapper mapper
        )
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public int GetCurrentUser()
        {
            //ClaimsPrincipal currentUser = HttpContext.User.FindFirstValue("id");

            //if (currentUser.Identity.IsAuthenticated)
            //{
            //    string userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //    string username = currentUser.Identity.Name;
            //}
            return 7;
        }
    }
}