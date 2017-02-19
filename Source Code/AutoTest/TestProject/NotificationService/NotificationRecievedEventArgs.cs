using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.NotificationService
{
    class NotificationRecievedEventArgs : EventArgs
    {
        public PushNotificationRecieveModel Notification { get; }

        public NotificationRecievedEventArgs(PushNotificationRecieveModel notification) : base()
        {
            this.Notification = notification;
        }
    }
}
