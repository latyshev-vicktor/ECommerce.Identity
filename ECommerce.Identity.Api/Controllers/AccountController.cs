using ECommerce.Api.Contracts;
using ECommerce.Api.Extensions;
using ECommerce.Application.UseCases.Users.Commands;
using ECommerce.Application.UseCases.Users.Queries;
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
            return result.AsHttpResult();
        }

        [HttpPost("login")]
        [SwaggerOperation("Авторизация пользователя")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var query = new LoginQuery(request.Email, request.Password);

            var result = await _mediator.Send(query);

            if (result.IsSuccess)
                HttpContext.Response.Cookies.Append("access_token", result.Value);

            return result.AsHttpResult();
        }
    }
}
