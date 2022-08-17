using System;
using System.Collections.Generic;
using System.Text;

namespace LiberacaoCredito.Devedor.Domain.Models.Notifications
{
    public class NotificationMessage
    {
        public NotificationMessage(string message)
        {
            Value = message;
        }

        public NotificationMessage(string type, string message)
        {
            Type = type;
            Value = message;
        }

        public string Type { get; set; }

        public string Value { get; private set; }
    }
}
