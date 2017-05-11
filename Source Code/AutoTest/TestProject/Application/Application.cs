using AutoTestEngine.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestProject.DAL;
using TestProject.NotificationService;

namespace TestProject.Application
{
    public class Application : IApplication
    {
        [Dependency]
        private INotificationService _notificationService;

        [Dependency]
        private IDAL _dal;
        //public Func<int, bool> ex;

        public Application(INotificationService notificationService, IDAL dal)
        {
            _notificationService = notificationService;
            _dal = dal;
        }

        public InvoiceModel SendInvoice(InvoiceModel invoice)
        {
            _dal.Create<InvoiceModel>(invoice);
            var notification = new PushNotificationSendModel()
            {
                NotificationText = "Invoice Created",
                Time = DateTime.Now
            };

            _notificationService.SendPushNotification(notification);
            invoice.IsProcessed = true;
            _dal.Create<InvoiceModel>(invoice);
            return invoice;
        }
    }
}
