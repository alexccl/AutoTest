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
        public void Repository_GetTypeRepository_2032353863()
        {
            var instance = (Repository)DeserializeObject(typeof(Repository), "{\"_dict\":{\"TestProject.Application.InvoiceModel, TestProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\":[{\"InvoiceId\":\"ca77a961-b89b-4ca6-87c3-281b1e05c17d\",\"IsProcessed\":true,\"<InvoiceId>k__BackingField\":\"ca77a961-b89b-4ca6-87c3-281b1e05c17d\",\"<IsProcessed>k__BackingField\":true}]}}");

            var testResult = instance.GetTypeRepository<InvoiceModel>();


            var expectedReturnVal = (List<InvoiceModel>)DeserializeObject(typeof(List<InvoiceModel>), "[{\"InvoiceId\":\"ca77a961-b89b-4ca6-87c3-281b1e05c17d\",\"IsProcessed\":true},{\"InvoiceId\":\"ca77a961-b89b-4ca6-87c3-281b1e05c17d\",\"IsProcessed\":true}]");
            var equalityResult = Compare(testResult, expectedReturnVal);
            Assert.IsTrue(equalityResult.AreEqual,
                        "Repository_GetTypeRepository_2032353863 failed testing equality with the message: " + equalityResult.DifferencesString);
        }
        //[TestMethod]
        //public void DAL_Create_478846846()
        //{
        //    var instance = (DAL)DeserializeObject(typeof(DAL), "{}");
        //
        //    InvoiceModel arg_InvoiceModel_0 = (InvoiceModel)DeserializeObject(typeof(InvoiceModel), "{\"InvoiceId\":\"4c2d27fc-9549-43df-91bd-f9fb57a1034e\",\"IsProcessed\":true}");
        //    var testResult = instance.Create<InvoiceModel>(arg_InvoiceModel_0);
        //
        //    var expectedReturnVal = (void)DeserializeObject(typeof(void), "null");
        //    var equalityResult = Compare(testResult, expectedReturnVal);
        //    Assert.IsTrue(equalityResult.AreEqual,
        //                "DAL_Create_478846846 failed testing equality with the message: " + equalityResult.DifferencesString);
        //}
        //[TestMethod]
        //public void NotificationService_SendPushNotification_478846846()
        //{
        //    var instance = (NotificationService)DeserializeObject(typeof(NotificationService), "{}");
        //
        //    PushNotificationSendModel arg_PushNotificationSendModel_0 = (PushNotificationSendModel)DeserializeObject(typeof(PushNotificationSendModel), "{\"NotificationGuid\":\"00000000-0000-0000-0000-000000000000\",\"NotificationText\":\"Invoice Created\",\"Time\":\"2017-02-27T17:11:54.8945198-05:00\",\"NotificationServer\":null}");
        //    var testResult = instance.SendPushNotification(arg_PushNotificationSendModel_0);
        //
        //    var expectedReturnVal = (void)DeserializeObject(typeof(void), "null");
        //    var equalityResult = Compare(testResult, expectedReturnVal);
        //    Assert.IsTrue(equalityResult.AreEqual,
        //                "NotificationService_SendPushNotification_478846846 failed testing equality with the message: " + equalityResult.DifferencesString);
        //}
        //[TestMethod]
        //public void Repository_GetTypeRepository_478846846()
        //{
        //    var instance = (Repository)DeserializeObject(typeof(Repository), "{}");
        //
        //    var testResult = instance.GetTypeRepository<InvoiceModel>();
        //
        //
        //    var expectedReturnVal = (List<InvoiceModel>)DeserializeObject(typeof(List<InvoiceModel>), "[{\"InvoiceId\":\"4c2d27fc-9549-43df-91bd-f9fb57a1034e\",\"IsProcessed\":true},{\"InvoiceId\":\"4c2d27fc-9549-43df-91bd-f9fb57a1034e\",\"IsProcessed\":true}]");
        //    var equalityResult = Compare(testResult, expectedReturnVal);
        //    Assert.IsTrue(equalityResult.AreEqual,
        //                "Repository_GetTypeRepository_478846846 failed testing equality with the message: " + equalityResult.DifferencesString);
        //}
        //[TestMethod]
        //public void DAL_Create_478846846()
        //{
        //    var instance = (DAL)DeserializeObject(typeof(DAL), "{}");
        //
        //    InvoiceModel arg_InvoiceModel_0 = (InvoiceModel)DeserializeObject(typeof(InvoiceModel), "{\"InvoiceId\":\"4c2d27fc-9549-43df-91bd-f9fb57a1034e\",\"IsProcessed\":true}");
        //    var testResult = instance.Create<InvoiceModel>(arg_InvoiceModel_0);
        //
        //    var expectedReturnVal = (void)DeserializeObject(typeof(void), "null");
        //    var equalityResult = Compare(testResult, expectedReturnVal);
        //    Assert.IsTrue(equalityResult.AreEqual,
        //                "DAL_Create_478846846 failed testing equality with the message: " + equalityResult.DifferencesString);
        //}
        [TestMethod]
        public void Application_SendInvoice_478846846()
        {
            var instance = (Application)DeserializeObject(typeof(Application), "{}");

            InvoiceModel arg_InvoiceModel_0 = (InvoiceModel)DeserializeObject(typeof(InvoiceModel), "{\"InvoiceId\":\"4c2d27fc-9549-43df-91bd-f9fb57a1034e\",\"IsProcessed\":true}");
            var testResult = instance.SendInvoice(arg_InvoiceModel_0);

            var expectedReturnVal = (InvoiceModel)DeserializeObject(typeof(InvoiceModel), "{\"InvoiceId\":\"4c2d27fc-9549-43df-91bd-f9fb57a1034e\",\"IsProcessed\":true}");
            var equalityResult = Compare(testResult, expectedReturnVal);
            Assert.IsTrue(equalityResult.AreEqual,
                        "Application_SendInvoice_478846846 failed testing equality with the message: " + equalityResult.DifferencesString);
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
            var settings = new JsonSerializerSettings() { ContractResolver = new MyContractResolver() };
            return JsonConvert.DeserializeObject(obj, t, settings);
        }
    }

    public class MyContractResolver : Newtonsoft.Json.Serialization.DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                            .Select(p => base.CreateProperty(p, memberSerialization))
                        .Union(type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                   .Select(f => base.CreateProperty(f, memberSerialization)))
                        .ToList();
            props.ForEach(p => { p.Writable = true; p.Readable = true; });
            return props;
        }
    }
}
