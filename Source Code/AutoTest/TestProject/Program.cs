using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Application;
using Microsoft.Practices.Unity;
using TestProject.DAL;
using Microsoft.Practices.Unity.InterceptionExtension;
using TestProject.NotificationService;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = new Dictionary<int, int>();
            var container = new UnityContainer();
            container.AddNewExtension<Interception>();
            container.RegisterType<IDAL, DAL.DAL>();
            container.RegisterType<IRepository, Repository>();
            container.RegisterType<INotificationService, NotificationService.NotificationService>();
            container.RegisterType<IApplication, Application.Application>();
            var app = container.Resolve<IApplication>();
            var invoice = new InvoiceModel()
            {
                InvoiceId = Guid.NewGuid(),
                IsProcessed = false
            };

            app.SendInvoice(invoice);
        }
    }
}
