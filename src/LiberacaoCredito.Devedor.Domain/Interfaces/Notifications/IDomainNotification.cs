using FluentValidation.Results;
using System.Collections.Generic;
using LiberacaoCredito.Devedor.Domain.Notifications;

namespace LiberacaoCredito.Devedor.Domain.Interfaces.Notifications
{
    public interface IDomainNotification
    {
        IReadOnlyCollection<NotificationMessage> Notifications { get; }
        bool HasNotifications { get; }
        void AddNotification(string key, string message);
        void AddNotifications(IEnumerable<NotificationMessage> notifications);
        void AddNotifications(ValidationResult validationResult);
    }
}
