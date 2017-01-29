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
            var guid = Guid.NewGuid();
            var subMethod = new RecordedSubMethod(guid);
            Assert.IsTrue(subMethod.Identifier.Equals(guid));
        }
    }
}
