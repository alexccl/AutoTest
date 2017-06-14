using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.NotificationService
{
    public interface INotificationService
    {
        void SendPushNotification(PushNotificationSendModel notificationModel);
        event EventHandler NotificationRecieved;
    }
}
