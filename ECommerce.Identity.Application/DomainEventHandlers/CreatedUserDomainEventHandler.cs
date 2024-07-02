using ECommerce.Domain.DomainEvents;
using MediatR;

namespace ECommerce.Application.DomainEventHandlers
{
    public class CreatedUserDomainEventHandler
        : INotificationHandler<CreatedUserEvent>
    {
        public Task Handle(CreatedUserEvent notification, CancellationToken cancellationToken)
        {
            //В дальнейшем будет обработка события при создании нового пользователя
            //Как пример отправка сообщения через MassTransit в API уведомлений

            return Task.CompletedTask;
        }
    }
}
