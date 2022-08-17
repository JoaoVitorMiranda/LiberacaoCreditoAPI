using FluentValidation.Results;
using System.Collections.Generic;
using LiberacaoCredito.Devedor.Domain.Models.Notifications;

namespace LiberacaoCredito.Devedor.Domain.Interfaces.Services
{
    public interface IServiceBase
    {
        IReadOnlyCollection<NotificationMessage> Notifications { get; }

        List<NotificationMessage> GetNotifications();

        bool HasNotifications { get; }

        void AddNotification(string message, string type = null);

        void AddNotifications(IEnumerable<NotificationMessage> notifications);

        void AddNotifications(ValidationResult validationResult);
    }
}
