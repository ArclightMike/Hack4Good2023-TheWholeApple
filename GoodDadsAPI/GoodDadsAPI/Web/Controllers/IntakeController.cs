using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GoodDads.Web.Controllers.Base;
using GoodDads.Services.Intake.SaveIntakeInformation;
using MediatR;

namespace GoodDads.Web.Controllers
{
	[ApiController]
    //[Authorize] when auth works.
    [Route("intake")]
	public sealed class IntakeController : GoodDadsController
	{
        private readonly IMediator mediator;

        public IntakeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("saveIntakeInformation")]
        [AllowAnonymous] //ToDo: DO NOT leave like this.  Should be [Authorize] when auth works.
        public async Task<ActionResult> SaveIntake(SaveIntakeRequest request)
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

