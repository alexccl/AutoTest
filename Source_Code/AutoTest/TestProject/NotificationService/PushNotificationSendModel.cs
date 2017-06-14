using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.NotificationService
{
    public class PushNotificationSendModel
    {
        public Guid NotificationGuid { get; set; }
        public string NotificationText { get; set; }
        public DateTime Time { get; set; }
        public Uri NotificationServer { get; set; }
    }
}
