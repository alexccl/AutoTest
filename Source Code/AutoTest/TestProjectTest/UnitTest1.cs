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
        public void Repository_GetTypeRepository_792697141()
        {
            var instance = (Repository)DeserializeObject(typeof(Repository), "{\"$type\":\"TestProject.DAL.Repository, TestProject\",\"_dict\":{\"$type\":\"System.Collections.Generic.Dictionary`2[[System.Type, mscorlib],[System.Object, mscorlib]], mscorlib\"}}");

            var testResult = instance.GetTypeRepository<InvoiceModel>();


            var expectedReturnVal = (List<InvoiceModel>)DeserializeObject(typeof(List<InvoiceModel>), "[]");
            var equalityResult = Compare(testResult, expectedReturnVal);
            Assert.IsTrue(equalityResult.AreEqual,
                        "Repository_GetTypeRepository_792697141 failed testing equality with the message: " + equalityResult.DifferencesString);
        }
        [TestMethod]
        public void Repository_GetTypeRepository_152377277()
        {
            var instance = (Repository)DeserializeObject(typeof(Repository), "{\"$type\":\"TestProject.DAL.Repository, TestProject\",\"_dict\":{\"$type\":\"System.Collections.Generic.Dictionary`2[[System.Type, mscorlib],[System.Object, mscorlib]], mscorlib\",\"TestProject.Application.InvoiceModel, TestProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\":{\"$type\":\"System.Collections.Generic.List`1[[TestProject.Application.InvoiceModel, TestProject]], mscorlib\",\"$values\":[{\"$type\":\"TestProject.Application.InvoiceModel, TestProject\",\"InvoiceId\":\"428f73d9-01d8-4a49-a197-698c6e0a0860\",\"IsProcessed\":true,\"<InvoiceId>k__BackingField\":\"428f73d9-01d8-4a49-a197-698c6e0a0860\",\"<IsProcessed>k__BackingField\":true}]}}}");

            var testResult = instance.GetTypeRepository<InvoiceModel>();


            var expectedReturnVal = (List<InvoiceModel>)DeserializeObject(typeof(List<InvoiceModel>), "[{\"InvoiceId\":\"428f73d9-01d8-4a49-a197-698c6e0a0860\",\"IsProcessed\":true}]");
            var equalityResult = Compare(testResult, expectedReturnVal);
            Assert.IsTrue(equalityResult.AreEqual,
                        "Repository_GetTypeRepository_152377277 failed testing equality with the message: " + equalityResult.DifferencesString);
        }
        [TestMethod]
        public void Application_SendInvoice_152377277()
        {
            var instance = (Application)DeserializeObject(typeof(Application), "{\"$type\":\"TestProject.Application.Application, TestProject\",\"_notificationService\":{\"$type\":\"DynamicModule.ns.Wrapped_INotificationService_26de3825049d40b28ab378556471b98f, Unity_ILEmit_InterfaceProxies\",\"pipeline\":{\"$type\":\"Microsoft.Practices.Unity.InterceptionExtension.InterceptionBehaviorPipeline, Microsoft.Practices.Unity.Interception\",\"Count\":1,\"interceptionBehaviors\":{\"$type\":\"System.Collections.Generic.List`1[[Microsoft.Practices.Unity.InterceptionExtension.IInterceptionBehavior, Microsoft.Practices.Unity.Interception]], mscorlib\",\"$values\":[{\"$type\":\"AutoTest4Unity.AutoTestBehavior, AutoTest4Unity\",\"Microsoft.Practices.Unity.InterceptionExtension.IInterceptionBehavior.WillExecute\":true}]}},\"target\":{\"$type\":\"TestProject.NotificationService.NotificationService, TestProject\",\"NotificationRecieved\":null},\"typeToProxy\":\"TestProject.NotificationService.INotificationService, TestProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\"},\"_dal\":{\"$type\":\"DynamicModule.ns.Wrapped_IDAL_75deb2bb0ee5414098760f33bd83d092, Unity_ILEmit_InterfaceProxies\",\"pipeline\":{\"$type\":\"Microsoft.Practices.Unity.InterceptionExtension.InterceptionBehaviorPipeline, Microsoft.Practices.Unity.Interception\",\"Count\":1,\"interceptionBehaviors\":{\"$type\":\"System.Collections.Generic.List`1[[Microsoft.Practices.Unity.InterceptionExtension.IInterceptionBehavior, Microsoft.Practices.Unity.Interception]], mscorlib\",\"$values\":[{\"$type\":\"AutoTest4Unity.AutoTestBehavior, AutoTest4Unity\",\"Microsoft.Practices.Unity.InterceptionExtension.IInterceptionBehavior.WillExecute\":true}]}},\"target\":{\"$type\":\"TestProject.DAL.DAL, TestProject\",\"_repository\":{\"$type\":\"DynamicModule.ns.Wrapped_IRepository_87d976b40bfa4fefa67f02900a44f156, Unity_ILEmit_InterfaceProxies\",\"pipeline\":{\"$type\":\"Microsoft.Practices.Unity.InterceptionExtension.InterceptionBehaviorPipeline, Microsoft.Practices.Unity.Interception\",\"Count\":1,\"interceptionBehaviors\":{\"$type\":\"System.Collections.Generic.List`1[[Microsoft.Practices.Unity.InterceptionExtension.IInterceptionBehavior, Microsoft.Practices.Unity.Interception]], mscorlib\",\"$values\":[{\"$type\":\"AutoTest4Unity.AutoTestBehavior, AutoTest4Unity\",\"Microsoft.Practices.Unity.InterceptionExtension.IInterceptionBehavior.WillExecute\":true}]}},\"target\":{\"$type\":\"TestProject.DAL.Repository, TestProject\",\"_dict\":{\"$type\":\"System.Collections.Generic.Dictionary`2[[System.Type, mscorlib],[System.Object, mscorlib]], mscorlib\"}},\"typeToProxy\":\"TestProject.DAL.IRepository, TestProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\"}},\"typeToProxy\":\"TestProject.DAL.IDAL, TestProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\"}}");

            InvoiceModel arg_InvoiceModel_0 = (InvoiceModel)DeserializeObject(typeof(InvoiceModel), "{\"InvoiceId\":\"e1d24a97-864b-4b31-a887-1bc65fbf41cd\",\"IsProcessed\":true}");
            var testResult = instance.SendInvoice(arg_InvoiceModel_0);

            var expectedReturnVal = (InvoiceModel)DeserializeObject(typeof(InvoiceModel), "{\"InvoiceId\":\"e1d24a97-864b-4b31-a887-1bc65fbf41cd\",\"IsProcessed\":true}");
            var equalityResult = Compare(testResult, expectedReturnVal);
            Assert.IsTrue(equalityResult.AreEqual,
                        "Application_SendInvoice_1516881835 failed testing equality with the message: " + equalityResult.DifferencesString);
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
                            .Select(p => base.CreateProperty(p, memberSerialization))
                        .Union(type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                   .Select(f => base.CreateProperty(f, memberSerialization)))
                        .ToList();
            props.ForEach(p => { p.Writable = true; p.Readable = true; });
            return props;
        }
    }
}
