using AutoTestEngine;
using AutoTestEngine.DAL.Models;
using AutoTestEngine.Helpers.Serialization;
using AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTest.Test.AutoTestEngineTests
{
    [TestClass]
    public class RecordedMethodModelTest
    {
        [TestMethod]
        public void Recorded_Method_Model_Test_Recording_Method_Init()
        {
            var guid = Guid.NewGuid();
            var targetType = TestClass.Method1Entry.TargetType;
            var methodName = TestClass.Method1Entry.Method.Name;
            var serTarget = "blah";
            var arg = new List<object>() {
               2.0,
               "blah"
            };
            var methodBase = TestClass.Method1Entry.Method;
            var returnVal = "foo";
            var serValue = new SerializedValue(targetType, serTarget);

            var recordingMethod = new RecordingMethod(guid, serValue, arg, methodBase);

            recordingMethod.CloseOutMethodWithReturnVal(returnVal);

            var SUT = new RecordedMethod(recordingMethod);

            for(int i = 0; i < SUT.Args.Count; i++)
            {
                Assert.IsTrue(SUT.Args[i].Equals(arg[i]));
            }
            Assert.IsTrue(SUT.Identifier.Equals(guid));
            Assert.IsTrue(SUT.InstanceAtExecutionTime.Value.Equals(serTarget));
            Assert.IsTrue(SUT.MethodException == null);
            Assert.IsTrue(SUT.MethodData.MethodName == methodName);
            Assert.IsTrue(SUT.ReturnVal.GetType().Equals( returnVal.GetType()));
            Assert.IsTrue(SUT.ReturnVal.Equals(returnVal));
            Assert.IsTrue(SUT.SubMethods != null);
            Assert.IsTrue(SUT.TargetType.Equals(targetType));
        }

        [TestMethod]
        public void Recorded_Method_Model_Test_Recording_Method_Id_Init()
        {
            var guid = Guid.NewGuid();
            var recMethod = new RecordedMethod(guid);
            Assert.IsTrue(guid == recMethod.Identifier);
        }

        [TestMethod]
        public void Recorded_Method_Model_Test_Equality_Override()
        {
            var guid = Guid.NewGuid();
            var targetType = TestClass.Method1Entry.TargetType;
            var methodName = TestClass.Method1Entry.Method.Name;
            var serTarget = "blah";
            var arg = new List<object>() {
               2.0,
               "blah"
            };
            var methodBase = TestClass.Method1Entry.Method;
            var returnVal = "foo";
            var serValue = new SerializedValue(targetType, serTarget);

            var recordingMethod = new RecordingMethod(guid, serValue, arg, methodBase);
            var recordedMethod = new RecordedMethod(recordingMethod);

            Assert.IsTrue(recordedMethod.Equals(recordedMethod));
            Assert.IsFalse(recordedMethod.Equals(new RecordedMethod(Guid.NewGuid())));
            Assert.IsTrue(recordedMethod.Equals(new RecordedMethod(guid)));

        }
    }
}
