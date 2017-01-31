using AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder;
using AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder.ExecutionCache;
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
    public class RecordingMethodManagerTest
    {
        private List<RecordedMethod> _methods 
        {
            get
            {
                return new List<RecordedMethod>() {
                    new RecordedMethod(typeof(int), "", null, TestClass.Method1Entry.Method)
                };

            }
        }

        public void Recorded_Method_Manager_Test_Thread_Id_Is_Used()
        {
            var mockThreadProvider = new Mock<IThreadIdProvider>();
            var mockCache = new Mock<IExecutionCache>();

            var threadId = 35;
            mockThreadProvider.Setup(x => x.GetThreadId()).Returns(threadId);
            mockCache.Setup(x => x.GetMethods(It.IsAny<int>())).Returns(_methods);

            var SUT = new RecordingMethodManager(mockCache.Object, mockThreadProvider.Object);

            SUT.ProcessCapture(TestClass.Method1Entry);
            mockCache.Verify(x => x.GetMethods(threadId), Times.Once);
        }
    }
}
