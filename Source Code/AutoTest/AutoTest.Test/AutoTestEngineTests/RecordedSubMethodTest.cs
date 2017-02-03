using AutoTestEngine.Helpers;
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
    public class RecordedSubMethodTest
    {
        [TestMethod]
        public void Recorded_Sub_Method_Identifier_Set()
        {
            var entry = TestClass.Method1Entry;
            var guid = Guid.NewGuid();
            var subMethod = new RecordedSubMethod(guid, entry.TargetType, entry.MethodArgs.ToTypeValList(), entry.Method.Name);
            Assert.IsTrue(subMethod.Identifier.Equals(guid));
        }

        [TestMethod]
        public void Recorded_Sub_Method_Test_Constructor_Initialization()
        {
            var entry = TestClass.Method1Entry;
            var guid = Guid.NewGuid();
            var subMethod = new RecordedSubMethod(guid, entry.TargetType, entry.MethodArgs.ToTypeValList(), entry.Method.Name);
            Assert.IsTrue(subMethod.Identifier.Equals(guid));
        }
    }
}
