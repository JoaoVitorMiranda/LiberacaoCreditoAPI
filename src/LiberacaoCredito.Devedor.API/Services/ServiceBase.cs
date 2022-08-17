using FluentValidation.Results;
using LiberacaoCredito.Devedor.Domain.Interfaces.Services;
using LiberacaoCredito.Devedor.Domain.Models.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace LiberacaoCredito.Devedor.API.Services
{
    public abstract class ServiceBase : IServiceBase
    {
        private readonly List<NotificationMessage> _notifications;

        public ServiceBase()
        {
            _notifications = new List<NotificationMessage>();
        }

        public IReadOnlyCollection<NotificationMessage> Notifications => _notifications;

        public bool HasNotifications => _notifications.Any();

        public void AddNotification(string message)
        {
            _notifications.Add(new NotificationMessage(message));
        }

        public void AddNotification(string type, string message)
        {
            _notifications.Add(new NotificationMessage(type, message));
        }

        public void AddNotifications(IEnumerable<NotificationMessage> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public void AddNotifications(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                _notifications.Add(new NotificationMessage(error.ErrorMessage));
            }
        }

        public List<NotificationMessage> GetNotifications()
        {
            return _notifications;
        }

        public void SetExternalNotifications(IServiceBase service)
        {
            if (service?.GetNotifications() != null)
                service.GetNotifications().ForEach(not => AddNotification(not.Type, not.Value));
        }

        public void SetFluentErrors(IList<ValidationFailure> errors)
        {
            if (errors == null)
                return;

            errors.ToList().ForEach(e => AddNotification(e.ErrorMessage));
        }
    }
}
