using MediatR;
using GoodDadsAPI.Services.Repositories;
using GoodDadsAPI.Services.Schema;

namespace GoodDads.Services.User.Login
{
    public sealed class RegisterRequest : IRequest<RegisterResponse>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public sealed class RegisterResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }
    }

    public sealed class RegisterRequestHandler : IRequestHandler<RegisterRequest, RegisterResponse>
    {
        private readonly ILoginRepository loginRepository;
        private readonly IDadRepository dadRepository;

        public RegisterRequestHandler(ILoginRepository loginRepository, IDadRepository dadRepository)
        {
            this.loginRepository = loginRepository;
            this.dadRepository = dadRepository;
        }

        public async Task<RegisterResponse> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Password)) 
            {
                throw new BadHttpRequestException("Password may not be null or empty.");
            }
        
            var login = await loginRepository.GetByEmail(request.Email);
            
            if (login is null)
            {
                return new RegisterResponse
                {
                    Success = false,
                    Message = "Please contact GoodDads for registration."
                };
            }

            if (login is not null && !string.IsNullOrEmpty(login.Password))
            {
                return new RegisterResponse
                {
                    Success = false,
                    Message = "This email address is already registered.  Please contact GoodDads for assistance."
                };
            }

            if (login is not null && string.IsNullOrEmpty(login.Password))
            {
                var user = await dadRepository.GetByEmail(request.Email);
            
                var loginData = new LoginSchema
                {
                    UserID = user.UserID,
                    Password = request.Password,
                };

                await loginRepository.UpdatePassword(loginData);
            }

            return new RegisterResponse
            {
                Success = true,
                Message = "Successfully registered."
            };
        }
    }
}

