using AutoMapper;
using Setia.Data;
using Setia.Services.Interfaces;

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

        public async Task<int> GetCurrentUser()
        {
            return 6;
        }
    }
}