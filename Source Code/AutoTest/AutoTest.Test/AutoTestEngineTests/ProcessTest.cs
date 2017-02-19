using AutoTestEngine;
using AutoTestEngine.DAL.Helpers;
using AutoTestEngine.DAL.Models;
using AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
        private IRecordedMethodHelper _methodHelper;
        private IRecordingMethodManager _manager;


        [TestInitialize]
        public void Setup()
        {
            var data = TestClass.Method1Entry;
            _entryModel = new InterceptionEntryModel(data.TargetInstance, data.MethodArgs, data.Method);
            _methodHelper = new Mock<IRecordedMethodHelper>().Object;
            _manager = new Mock<IRecordingMethodManager>().Object;
        }

        [TestMethod]
        public void Execution_Recorder_Test_Priority()
        {
            var SUT = new ExecutionRecorderProcess(_manager, _methodHelper);

            //this process should atleast come after unit test process
            Assert.IsTrue(SUT.ProcessPriority > 1);
        }

        [TestMethod]
        public void Execution_Recorder_Unit_Test_Config_Should_Not_Run_Process()
        {
            var SUT = new ExecutionRecorderProcess(_manager, _methodHelper);

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
            var SUT = new ExecutionRecorderProcess(_manager, _methodHelper);

            var config = new EngineConfiguration()
            {
                IsUnitTesting = false
            };

            var procData = new InterceptionProcessingData(_entryModel, config);

            Assert.IsTrue(SUT.ShouldExecuteProcess(procData));
        }

        [TestMethod]
        public void Execution_Recorder_Finished_Method_Should_Be_Added_To_DAL()
        {
            var _helper = new Mock<IRecordedMethodHelper>();
            var _manager = new Mock<IRecordingMethodManager>();

            var entry = TestClass.Method1Entry;

            var recMethod = new RecordingMethod(Guid.NewGuid(), entry.TargetType, "", entry.MethodArgs.ToArray(), entry.Method);
            recMethod.CloseOutMethodWithReturnVal(TestClass.Method1Exit.ReturnValue);

            _manager.Setup(x => x.ProcessCapture(It.IsAny<InterceptionProcessingData>())).Raises(f => f.MethodRecordingComplete += null, new MethodRecordingCompleteEventArgs(recMethod));

            var SUT = new ExecutionRecorderProcess(_manager.Object, _helper.Object);
            SUT.ExecuteProcess(TestClass.Method1Entry);

            _helper.Verify(x => x.AddRecordedMethod(It.IsAny<RecordedMethod>()), Times.Once);
        }

        [TestMethod]
        public void Execution_Recorder_Override_Value_Null()
        {
            var _helper = new Mock<IRecordedMethodHelper>();
            var _manager = new Mock<IRecordingMethodManager>();

            var SUT = new ExecutionRecorderProcess(_manager.Object, _helper.Object);
            var res = SUT.ExecuteProcess(TestClass.Method1Entry);

            Assert.IsTrue(res.OverrideValue == null);
        }
    }
}
