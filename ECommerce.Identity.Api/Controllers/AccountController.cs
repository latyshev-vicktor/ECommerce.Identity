using ECommerce.Api.Contracts;
using ECommerce.Application.UseCases.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ECommerce.Api.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AccountController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("registerUser")]
        [SwaggerOperation("Регистрация нового пользователя")]
        public async Task<IActionResult> RegisterUser([FromBody]CreateUserRequest request)
        {
            var command = new CreateUserCommand(request.UserName, request.FirstName, 
                                                request.LastName, request.Email, request.Password, 
                                                request.UserType, request.RoleIds);

            var result = await _mediator.Send(command);

            //TODO: вынести в какой-нибудь extension метод
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }
}
