using ECommerce.Application.UseCases.Users.Queries;
using ECommerce.Domain.DTO.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ECommerce.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator  = mediator;

        [HttpGet("[action]")]
        [SwaggerOperation("Получение списка пользователей")]
        [AllowAnonymous]
        public async Task<IReadOnlyList<UserDto>> GetAllUsers()
        {
            var query = new GetAllUsersQuery();

            var result = await _mediator.Send(query);
            return result;
        }
    }
}
