using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.NotificationService
{
    class NotificationService : INotificationService
    {
        public event EventHandler NotificationRecieved;

        public void SendPushNotification(PushNotificationSendModel notificationModel)
        {
            var notificationRecievedModel = new PushNotificationRecieveModel()
            {
                NotificationText = notificationModel.NotificationText,
                Time = notificationModel.Time
            };

            var args = new NotificationRecievedEventArgs(notificationRecievedModel);
            if (NotificationRecieved != null)
                NotificationRecieved(this, args);
        }
    }
}
