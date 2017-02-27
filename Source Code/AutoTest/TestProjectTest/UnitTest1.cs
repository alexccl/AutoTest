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

namespace TestProjectTest
{
    [TestClass]
    public class AutoTest_Generated_Tests
    {
        [TestMethod]
        public void IRepository_GetTypeRepository_248693268()
        {
            var instance = (IRepository)DeserializeObject(typeof(IRepository), "{}");

            var testResult = instance.GetTypeRepository();


            var expectedReturnVal = (List<InvoiceModel>)DeserializeObject(typeof(List<InvoiceModel>), "[{\"InvoiceId\":\"eda98477-dc5d-449e-96f9-d0ba13724108\",\"IsProcessed\":true},{\"InvoiceId\":\"eda98477-dc5d-449e-96f9-d0ba13724108\",\"IsProcessed\":true}]");
            var equalityResult = Compare(testResult, expectedReturnVal);
            Assert.IsTrue(equalityResult.AreEqual,
                        IRepository_GetTypeRepository_248693268 + " failed testing equality with the message: " + res.DifferencesString);
        }
        [TestMethod]
        public void IDAL_Create_345009259()
        {
            var instance = (IDAL)DeserializeObject(typeof(IDAL), "{}");
            
            InvoiceModel arg_InvoiceModel_0 = (InvoiceModel)DeserializeObject(InvoiceModel, "{\"InvoiceId\":\"eda98477-dc5d-449e-96f9-d0ba13724108\",\"IsProcessed\":true}");
            var testResult = instance.Create(arg_InvoiceModel_0);

            var expectedReturnVal = (void)DeserializeObject(typeof(void), "null");
            var equalityResult = Compare(testResult, expectedReturnVal);
            Assert.IsTrue(equalityResult.AreEqual,
                        IDAL_Create_345009259 + " failed testing equality with the message: " + res.DifferencesString);
        }
        [TestMethod]
        public void INotificationService_SendPushNotification_345009259()
        {
            var instance = (INotificationService)DeserializeObject(typeof(INotificationService), "{}");

            PushNotificationSendModel arg_PushNotificationSendModel_0 = (PushNotificationSendModel)DeserializeObject(PushNotificationSendModel, "{\"NotificationGuid\":\"00000000-0000-0000-0000-000000000000\",\"NotificationText\":\"Invoice Created\",\"Time\":\"2017-02-23T12:58:06.3916329-05:00\",\"NotificationServer\":null}");
            var testResult = instance.SendPushNotification(arg_PushNotificationSendModel_0);

            var expectedReturnVal = (void)DeserializeObject(typeof(void), "null");
            var equalityResult = Compare(testResult, expectedReturnVal);
            Assert.IsTrue(equalityResult.AreEqual,
                        INotificationService_SendPushNotification_345009259 + " failed testing equality with the message: " + res.DifferencesString);
        }
        [TestMethod]
        public void IRepository_GetTypeRepository_345009259()
        {
            var instance = (IRepository)DeserializeObject(typeof(IRepository), "{}");

            var testResult = instance.GetTypeRepository();


            var expectedReturnVal = (List<InvoiceModel>)DeserializeObject(typeof(List<InvoiceModel>), "[{\"InvoiceId\":\"eda98477-dc5d-449e-96f9-d0ba13724108\",\"IsProcessed\":true},{\"InvoiceId\":\"eda98477-dc5d-449e-96f9-d0ba13724108\",\"IsProcessed\":true}]");
            var equalityResult = Compare(testResult, expectedReturnVal);
            Assert.IsTrue(equalityResult.AreEqual,
                        IRepository_GetTypeRepository_345009259 + " failed testing equality with the message: " + res.DifferencesString);
        }
        [TestMethod]
        public void IDAL_Create_345009259()
        {
            var instance = (IDAL)DeserializeObject(typeof(IDAL), "{}");

            InvoiceModel arg_InvoiceModel_0 = (InvoiceModel)DeserializeObject(InvoiceModel, "{\"InvoiceId\":\"eda98477-dc5d-449e-96f9-d0ba13724108\",\"IsProcessed\":true}");
            var testResult = instance.Create(arg_InvoiceModel_0);

            var expectedReturnVal = (void)DeserializeObject(typeof(void), "null");
            var equalityResult = Compare(testResult, expectedReturnVal);
            Assert.IsTrue(equalityResult.AreEqual,
                        IDAL_Create_345009259 + " failed testing equality with the message: " + res.DifferencesString);
        }
        [TestMethod]
        public void IApplication_SendInvoice_345009259()
        {
            var instance = (IApplication)DeserializeObject(typeof(IApplication), "{}");

            InvoiceModel arg_InvoiceModel_0 = (InvoiceModel)DeserializeObject(typeof(InvoiceModel), "{\"InvoiceId\":\"eda98477-dc5d-449e-96f9-d0ba13724108\",\"IsProcessed\":true}");
            var testResult = instance.SendInvoice(arg_InvoiceModel_0);

            var expectedReturnVal = (InvoiceModel)DeserializeObject(typeof(InvoiceModel), "{\"InvoiceId\":\"eda98477-dc5d-449e-96f9-d0ba13724108\",\"IsProcessed\":true}");
            var equalityResult = Compare(testResult, expectedReturnVal);
            Assert.IsTrue(equalityResult.AreEqual,
                        IApplication_SendInvoice_345009259 + " failed testing equality with the message: " + res.DifferencesString);
        }

        private string ComparisonResult Compare(object obj1, object obj2)
        {
            CompareLogic compareLogic = new CompareLogic();
            return compareLogic.Compare()

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
            var method = typeof(JsonConvert).GetMethod("DeserializeObject").MakeGenericMethod(t);
            return method.Invoke(null, obj);
        }
    }
}
