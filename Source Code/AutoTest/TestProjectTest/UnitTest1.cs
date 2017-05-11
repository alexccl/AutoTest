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
            var instance = (Repository)DeserializeObject(typeof(Repository), "{\"$type\":\"TestProject.DAL.Repository, TestProject\",\"_dict\":{\"$type\":\"System.Collections.Generic.Dictionary`2[[System.Type, mscorlib],[System.Object, mscorlib]], mscorlib\",\"TestProject.Application.InvoiceModel, TestProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\":{\"$type\":\"System.Collections.Generic.List`1[[TestProject.Application.InvoiceModel, TestProject]], mscorlib\",\"$values\":[{\"$type\":\"TestProject.Application.InvoiceModel, TestProject\",\"InvoiceId\":\"f9832d9d-1b1f-4ac5-b3ab-f1b6bdd4c661\",\"IsProcessed\":true,\"<InvoiceId>k__BackingField\":\"f9832d9d-1b1f-4ac5-b3ab-f1b6bdd4c661\",\"<IsProcessed>k__BackingField\":true}]}}}");

            var testResult = instance.GetTypeRepository<InvoiceModel>();


            var expectedReturnVal = (List<InvoiceModel>)DeserializeObject(typeof(List<InvoiceModel>), "[{\"InvoiceId\":\"f9832d9d-1b1f-4ac5-b3ab-f1b6bdd4c661\",\"IsProcessed\":true}]");
            var equalityResult = Compare(testResult, expectedReturnVal);
            Assert.IsTrue(equalityResult.AreEqual,
                        "Repository_GetTypeRepository_1115537187 failed testing equality with the message: " + equalityResult.DifferencesString);
        }

        private ComparisonResult Compare(object obj1, object obj2)
        {
            CompareLogic compareLogic = new CompareLogic();
            return compareLogic.Compare(obj1, obj2);
        }

        private object SetPropertyOnType(Type classType, object classInstance, Type propertyType, object propertyInstance)
        {
            var props = classType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var prop in props)
            {
                if (prop.PropertyType != propertyType) continue;

                prop.SetValue(classInstance, propertyInstance);
                return classInstance;
            }

            var fields = classType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach(var field in fields)
            {
                if (field.FieldType != propertyType) continue;

                field.SetValue(classInstance, propertyInstance);
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
