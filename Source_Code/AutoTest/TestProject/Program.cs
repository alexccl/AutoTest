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
using AutoTest4Unity;
using TestProject.Helpers;

namespace TestProject
{
class Program
{
    static void Main(string[] args)
    {
        //register types and interceptors with the IOC container
        var container = new UnityContainer();
        container.AddNewExtension<Interception>();
        container.RegisterType<IDAL, DAL.DAL>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<AutoTestBehavior>());
        container.RegisterType<IRepository, Repository>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<AutoTestBehavior>());
        container.RegisterType<IExample, Example>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<AutoTestBehavior>());

        //resolve IOC dependencies and construct object
        var example = container.Resolve<IExample>();

        var result = example.AddNumberToDBVal(2);
        Console.WriteLine($"Adding 2 to the database value yields: {result}");
    }
}
}
