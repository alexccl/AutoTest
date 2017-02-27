using AutoTestEngine.DAL.Models;
using AutoTestEngine.TestGeneration;
using AutoTestEngine.TestGeneration.Generation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTest.Test.AutoTestEngineTests
{
    [TestClass]
    public class GenerationDataTest
    {
        [TestMethod]
        public void Generation_Data_Test_Initialization_Of_Object()
        {
            var genData = new SingleTest(TestRecordedMethod.Method1);

            Assert.IsTrue(genData.InstanceType == TestClass.Method1Entry.TargetInstance.Type);
            Assert.IsTrue(genData.ObjectInstance == TestClass.Method1EntryRecording.InstanceAtExecutionTime.Value);
            Assert.IsTrue(genData.TestName != null);
            Assert.IsTrue(genData.Dependencies.Count == 2);
            Assert.IsTrue(genData.WasExceptionThrown == false);
            Assert.IsTrue(genData.ThrownException == null);
            Assert.IsTrue(genData.ReturnVal.Type.Equals(typeof(string)));
            Assert.IsTrue(genData.ReturnVal.Value.Equals("blah"));
            Assert.IsTrue(genData.MethodName == TestClass.Method1Entry.Method.Name);
            Assert.IsTrue(genData.SerializedReturnVal != null);

            var arg = genData.Args[0];
            Assert.IsTrue(arg.GeneratedArgName != null);
            Assert.IsTrue(arg.SerializedArgInstance == "1");
            Assert.IsTrue(arg.Type.Equals(typeof(int)));

            arg = genData.Args[1];
            Assert.IsTrue(arg.GeneratedArgName != null);
            Assert.IsTrue(arg.SerializedArgInstance != null);
            Assert.IsTrue(arg.Type.Equals(typeof(DateTime)));

            var dep = genData.Dependencies.FirstOrDefault(x => x.MemberType == typeof(ITestSubClass));

            Assert.IsTrue(dep != null);
            Assert.IsTrue(dep.Methods.Count == 1);

            var method = dep.Methods[0];
            Assert.IsTrue(method.MethodArgs.Count == 1);
            Assert.IsTrue(method.MethodName == "Method1");
            Assert.IsTrue(method.MethodCallReturs.Count == 2);

            var methodCall = method.MethodCallReturs[0];
            Assert.IsTrue(methodCall.ExceptionThrown == false);
            Assert.IsTrue(methodCall.ReturnVal.Type == typeof(int));
            Assert.IsTrue(methodCall.ReturnVal.Value.Equals(1));
            Assert.IsTrue(methodCall.Exception == null);

            methodCall = method.MethodCallReturs[1];
            Assert.IsTrue(methodCall.ExceptionThrown == true);
            Assert.IsTrue(methodCall.ReturnVal == null);
            Assert.IsTrue(methodCall.Exception != null);

            dep = genData.Dependencies.FirstOrDefault(x => x.MemberType == typeof(ITestSubClass2));

            Assert.IsTrue(dep != null);
            Assert.IsTrue(dep.Methods.Count == 1);

            method = dep.Methods[0];
            Assert.IsTrue(method.MethodArgs.Count == 1);
            Assert.IsTrue(method.MethodName == "Method1");
            Assert.IsTrue(method.MethodCallReturs.Count == 1);

            methodCall = method.MethodCallReturs[0];
            Assert.IsTrue(methodCall.ExceptionThrown == false);
            Assert.IsTrue(methodCall.ReturnVal.Type == typeof(int));
            Assert.IsTrue(methodCall.ReturnVal.Value.Equals(2));
            Assert.IsTrue(methodCall.Exception == null);

        }

        [TestMethod]
        public void AAATestFireGenerator()
        {
            var testData = new TestData(new List<RecordedMethod>() { TestRecordedMethod.Method1 });
            var generator = new Generator();
            generator.Generate(testData);
        }
    }
}
