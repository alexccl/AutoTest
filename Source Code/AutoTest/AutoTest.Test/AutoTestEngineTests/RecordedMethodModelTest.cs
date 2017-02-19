using AutoTestEngine.DAL.Models;
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
            var arg = new object[] { 2.0, "blah" };
            var methodBase = TestClass.Method1Entry.Method;
            var returnVal = "foo";

            var recordingMethod = new RecordingMethod(guid, targetType, serTarget, arg, methodBase);

            recordingMethod.CloseOutMethodWithReturnVal(returnVal);

            var SUT = new RecordedMethod(recordingMethod);

            for(int i = 0; i < SUT.Args.Count; i++)
            {
                Assert.IsTrue(SUT.Args[i].Value.Equals(arg[i]));
            }
            Assert.IsTrue(SUT.Identifier.Equals(guid));
            Assert.IsTrue(SUT.InstanceAtExecutionTime.Equals(serTarget));
            Assert.IsTrue(SUT.MethodException == null);
            Assert.IsTrue(SUT.MethodName == methodName);
            Assert.IsTrue(SUT.ReturnTypeVal.Type.Equals( returnVal.GetType()));
            Assert.IsTrue(SUT.ReturnTypeVal.Value.Equals(returnVal));
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
            var arg = new object[] { 2.0, "blah" };
            var methodBase = TestClass.Method1Entry.Method;
            var returnVal = "foo";

            var recordingMethod = new RecordingMethod(guid, targetType, serTarget, arg, methodBase);
            var recordedMethod = new RecordedMethod(recordingMethod);

            Assert.IsTrue(recordedMethod.Equals(recordedMethod));
            Assert.IsFalse(recordedMethod.Equals(new RecordedMethod(Guid.NewGuid())));
            Assert.IsTrue(recordedMethod.Equals(new RecordedMethod(guid)));

        }
    }
}
