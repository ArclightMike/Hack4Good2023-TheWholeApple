using MediatR;
using GoodDadsAPI.Services.Repositories;

namespace GoodDads.Services.User.Login
{
    public sealed class LoginRequest : IRequest<LoginResponse>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public sealed class LoginResponse
    {
        public bool Success { get; set; }
    }

    public sealed class LoginRequestHandler : IRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly ILoginRepository loginRepository;

        public LoginRequestHandler(ILoginRepository loginRepository)
        {
            this.loginRepository = loginRepository;
        }

        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            //ToDo: All of this needs replaced with real authentication.
        
            var login = await loginRepository.GetByEmail(request.Email);

            var validLogin = false;
            
            if (login is not null && login.Password == request.Password)
            {
                validLogin = true;
            }

            return new LoginResponse
            {
                Success = validLogin,
            };
        }
    }
}

