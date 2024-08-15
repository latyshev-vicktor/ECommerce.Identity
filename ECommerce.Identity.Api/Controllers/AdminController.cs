using ECommerce.Api.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Application.UseCases.Users.Commands;
using ECommerce.Api.Extensions;
using Swashbuckle.AspNetCore.Annotations;

namespace ECommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPut("[action]")]
        [SwaggerOperation("Назначить роли для пользователя")]
        public async Task<IActionResult> SetRolesForUser([FromBody] SetRolesForUserRequest request)
        {
            var command = new SetRolesForUserCommand(request.UserId, request.RoleIds);
            var result = await _mediator.Send(command);

            return result.AsHttpResult();
        }

        [HttpPut("[action]")]
        [SwaggerOperation("Удалить роли для польщователя")]
        public async Task<IActionResult> RemoveRolesForUser([FromBody] RemoveRolesForUserRequest request)
        {
            var command = new RemoveRolesForUserCommand(request.UserId, request.RoleIds);
            var result = await _mediator.Send(command);

            return result.AsHttpResult();
        }
    }
}
