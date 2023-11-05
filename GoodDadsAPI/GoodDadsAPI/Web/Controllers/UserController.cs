using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GoodDads.Web.Controllers.Base;
using MediatR;
using GoodDads.Services.User.Login;

namespace GoodDads.Web.Controllers
{
	[ApiController]
    //[Authorize] when auth works.
    [Route("user")]
	public sealed class UserController : GoodDadsController
	{
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("login")]
        [AllowAnonymous] //ToDo: Set up real authentication.
        public async Task<ActionResult> Login(LoginRequest request)
        {
            try
            {
                var response = await this.mediator.Send(request);

                return this.Ok(response);
            }
            catch (Exception ex) //ToDo: set up requestexception
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        [AllowAnonymous] //ToDo: DO NOT leave like this.  Should be [Authorize] when auth works.
        public async Task<ActionResult> Register(RegisterRequest request)
        {
            try
            {
                var response = await this.mediator.Send(request);

                return this.Ok(response);
            }
            catch (Exception ex) //ToDo: set up requestexception
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}

