using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using Newtonsoft.Json;
using Moq;
using KellermanSoftware.CompareNetObjects;
using TestProject.DAL;
using TestProject.Application;
using System.Collections.Generic;
using System.Collections;
using TestProject.NotificationService;
using Newtonsoft.Json.Serialization;
using System.Linq;

namespace TestProjectTest
{
    [TestClass]
    public class AutoTest_Generated_Tests
    {
		[TestMethod]
		public void Application_SendInvoice_1279637324()
		{
            var instance = (Application)DeserializeObject(typeof(Application), "{\"$type\":\"TestProject.Application.Application, TestProject\",\"_notificationService\":{\"$type\":\"DynamicModule.ns.Wrapped_INotificationService_c5a385e02e894e8d9297cf8459973ad7, Unity_ILEmit_InterfaceProxies\"},\"_dal\":{\"$type\":\"DynamicModule.ns.Wrapped_IDAL_962102704dd7474a95d9cb6ace699ea2, Unity_ILEmit_InterfaceProxies\"}}");

            var mock_IDAL = new Mock<IDAL>();
            mock_IDAL.Setup(x => x.Create<InvoiceModel>(It.IsAny<InvoiceModel>()));

            instance = (Application)SetPropertyOnType(typeof(Application), instance, typeof(IDAL), mock_IDAL.Object);

            var mock_INotificationService = new Mock<INotificationService>();
            mock_INotificationService.Setup(x => x.SendPushNotification(It.IsAny<PushNotificationSendModel>()));

            instance = (Application)SetPropertyOnType(typeof(Application), instance, typeof(INotificationService), mock_INotificationService.Object);

            InvoiceModel arg_InvoiceModel_0 = (InvoiceModel)DeserializeObject(typeof(InvoiceModel), "{\"InvoiceId\":\"e0c23413-4a2b-4059-aea4-a36cd1fd5efb\",\"IsProcessed\":true}");
            var testResult = instance.SendInvoice(arg_InvoiceModel_0);

            var expectedReturnVal = (InvoiceModel)DeserializeObject(typeof(InvoiceModel), "{\"InvoiceId\":\"e0c23413-4a2b-4059-aea4-a36cd1fd5efb\",\"IsProcessed\":true}");
            var equalityResult = Compare(testResult, expectedReturnVal);
            Assert.IsTrue(equalityResult.AreEqual,
                        "Application_SendInvoice_1518612540 failed testing equality with the message: " + equalityResult.DifferencesString);
        }

        private ComparisonResult Compare(object obj1, object obj2)
        {
            CompareLogic compareLogic = new CompareLogic();
            return compareLogic.Compare(obj1, obj2);
        }

        private object SetPropertyOnType(Type classType, object classInstance, Type propertyType, object propertyInstance)
        {
            var props = classType.GetProperties();
            foreach (var prop in props)
            {
                if (prop.PropertyType != propertyType) continue;

                prop.SetValue(classInstance, propertyInstance);
                return classInstance;
            }

            throw new Exception("Could not find type: " + propertyType.Name + " on type: " + classType.Name);
        }

        private object DeserializeObject(Type t, string obj)
        {
            var settings = new JsonSerializerSettings() { ContractResolver = new MyContractResolver(), TypeNameHandling = TypeNameHandling.All };
            return JsonConvert.DeserializeObject(obj, t, settings);
        }
    }

    public class MyContractResolver : Newtonsoft.Json.Serialization.DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                            .Select(p => CreateProperty(p, memberSerialization))
                        .Union(type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                   .Select(f => CreateProperty(f, memberSerialization)))
                        .ToList();
            props.ForEach(p => { p.Writable = true; p.Readable = true; });
            return props;
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            property.ShouldSerialize = instance =>
            {
                try
                {
                    PropertyInfo prop = (PropertyInfo)member;
                    if (prop.CanRead)
                    {
                        prop.GetValue(instance, null);
                        return true;
                    }
                }
                catch(Exception ex)
                {

                }
                return false;
            };

            return property;
        }
    }
}
