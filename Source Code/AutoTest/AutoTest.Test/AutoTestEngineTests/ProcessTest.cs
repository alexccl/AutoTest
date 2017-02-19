using AutoTestEngine;
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
    public class ProcessTest
    {

        private InterceptionEntryModel _entryModel;

        [TestInitialize]
        public void Setup()
        {
            var data = TestClass.Method1Entry;
            _entryModel = new InterceptionEntryModel(data.TargetInstance, data.MethodArgs, data.Method);
        }

        [TestMethod]
        public void Execution_Recorder_Test_Priority()
        {
            var SUT = new ExecutionRecorderProcess(null);

            //this process should atleast come after unit test process
            Assert.IsTrue(SUT.ProcessPriority > 1);
        }

        [TestMethod]
        public void Execution_Recorder_Unit_Test_Config_Should_Not_Run_Process()
        {
            var SUT = new ExecutionRecorderProcess(null);

            var config = new EngineConfiguration()
            {
                IsUnitTesting = true
            };

            var procData = new InterceptionProcessingData(_entryModel, config);

            Assert.IsFalse(SUT.ShouldExecuteProcess(procData));
        }

        [TestMethod]
        public void Execution_Recorder_Non_Unit_Test_Config_Should_Run_Process()
        {
            var SUT = new ExecutionRecorderProcess(null);

            var config = new EngineConfiguration()
            {
                IsUnitTesting = true
            };

            var procData = new InterceptionProcessingData(_entryModel, config);

            Assert.IsTrue(SUT.ShouldExecuteProcess(procData));
        }
    }
}
