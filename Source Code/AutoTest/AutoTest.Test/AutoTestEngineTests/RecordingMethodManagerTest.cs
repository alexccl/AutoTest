using AutoTestEngine.Helpers.Serialization;
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
        private bool _methodCompleteEventRaised = false;
        private MethodRecordingCompleteEventArgs _eventArgs;

        private SerializationResult _serResult = SerializationResult.InitSuccessfulSerialization(null, "serialized object");

        [TestCleanup]
        public void CleanupAfterTest()
        {
            _methodCompleteEventRaised = false;
            _eventArgs = null;
        }



        private List<RecordingMethod> _methods 
        {
            get
            {
                return new List<RecordingMethod>() {
                    new RecordingMethod(typeof(int), "", null, TestClass.Method1Entry.Method)
                };

            }
        }

        [TestMethod]
        public void Recorded_Method_Manager_Test_Thread_Id_Is_Used_On_Execution_Cache()
        {
            var mockThreadProvider = new Mock<IThreadIdProvider>();
            var mockCache = new Mock<IExecutionCache>();
            var mockStack = new Mock<IExecutionStack>();
            var mockSerializer = new Mock<ISerializationHelper>();

            var threadId = 35;
            mockThreadProvider.Setup(x => x.GetThreadId()).Returns(threadId);
            mockCache.Setup(x => x.GetMethods(It.IsAny<int>())).Returns(_methods);
            mockSerializer.Setup(x => x.Serialize(It.IsAny<Object>())).Returns(_serResult);

            var SUT = new RecordingMethodManager(mockCache.Object, mockThreadProvider.Object, mockStack.Object, mockSerializer.Object);

            SUT.ProcessCapture(TestClass.Method1Entry);
            mockCache.Verify(x => x.GetMethods(threadId), Times.Once);
        }

        [TestMethod]
        public void Recorded_Method_Manager_Test_Thread_Id_Is_Used_On_Execution_Stack()
        {
            var mockThreadProvider = new Mock<IThreadIdProvider>();
            var mockCache = new Mock<IExecutionCache>();
            var mockStack = new Mock<IExecutionStack>();
            var mockSerializer = new Mock<ISerializationHelper>();

            var threadId = 35;
            mockThreadProvider.Setup(x => x.GetThreadId()).Returns(threadId);
            mockCache.Setup(x => x.GetMethods(It.IsAny<int>())).Returns(_methods);
            mockSerializer.Setup(x => x.Serialize(It.IsAny<Object>())).Returns(_serResult);

            var SUT = new RecordingMethodManager(mockCache.Object, mockThreadProvider.Object, mockStack.Object, mockSerializer.Object);

            SUT.ProcessCapture(TestClass.Method1Entry);
            mockStack.Verify(x => x.ProcessEntry(threadId, It.IsAny<Guid>()), Times.Once);
        }

        [TestMethod]
        public void Recorded_Method_Manager_Method_Exit_Raises_Complete_Event()
        {
            var mockThreadProvider = new Mock<IThreadIdProvider>();
            var mockCache = new Mock<IExecutionCache>();
            var mockStack = new Mock<IExecutionStack>();
            var mockSerializer = new Mock<ISerializationHelper>();

            var threadId = 35;
            mockThreadProvider.Setup(x => x.GetThreadId()).Returns(threadId);

            var _methods = new List<RecordingMethod>() { new RecordingMethod(typeof(int), "", null, TestClass.Method1Entry.Method) };
            mockCache.Setup(x => x.GetMethods(It.IsAny<int>())).Returns(_methods);
            mockStack.Setup(x => x.ExecutingGuid(It.IsAny<int>())).Returns(_methods.FirstOrDefault().Identifier);
            mockSerializer.Setup(x => x.Serialize(It.IsAny<Object>())).Returns(_serResult);

            var SUT = new RecordingMethodManager(mockCache.Object, mockThreadProvider.Object, mockStack.Object, mockSerializer.Object);
            SUT.MethodRecordingComplete += SUT_MethodRecordingComplete;
            SUT.ProcessCapture(TestClass.Method1Exit);

            Assert.IsTrue(_methodCompleteEventRaised == true);
        }

        [TestMethod]
        public void Recorded_Method_Manager_Method_Exception_Raises_Complete_Event()
        {
            var mockThreadProvider = new Mock<IThreadIdProvider>();
            var mockCache = new Mock<IExecutionCache>();
            var mockStack = new Mock<IExecutionStack>();
            var mockSerializer = new Mock<ISerializationHelper>();

            var threadId = 35;
            mockThreadProvider.Setup(x => x.GetThreadId()).Returns(threadId);

            var _methods = new List<RecordingMethod>() { new RecordingMethod(typeof(int), "", null, TestClass.Method1Entry.Method) };
            mockCache.Setup(x => x.GetMethods(It.IsAny<int>())).Returns(_methods);
            mockStack.Setup(x => x.ExecutingGuid(It.IsAny<int>())).Returns(_methods.FirstOrDefault().Identifier);
            mockSerializer.Setup(x => x.Serialize(It.IsAny<Object>())).Returns(_serResult);

            var SUT = new RecordingMethodManager(mockCache.Object, mockThreadProvider.Object, mockStack.Object, mockSerializer.Object);
            SUT.MethodRecordingComplete += SUT_MethodRecordingComplete;
            SUT.ProcessCapture(TestClass.Method1Exception);

            Assert.IsTrue(_methodCompleteEventRaised == true);
        }

        [TestMethod]
        public void Recorded_Method_Manager_Method_Entry_Wont_Raise_Complete_Event()
        {
            var mockThreadProvider = new Mock<IThreadIdProvider>();
            var mockCache = new Mock<IExecutionCache>();
            var mockStack = new Mock<IExecutionStack>();
            var mockSerializer = new Mock<ISerializationHelper>();

            var threadId = 35;
            mockThreadProvider.Setup(x => x.GetThreadId()).Returns(threadId);

            var _methods = new List<RecordingMethod>() { new RecordingMethod(typeof(int), "", null, TestClass.Method1Entry.Method) };
            mockCache.Setup(x => x.GetMethods(It.IsAny<int>())).Returns(_methods);
            mockStack.Setup(x => x.ExecutingGuid(It.IsAny<int>())).Returns(_methods.FirstOrDefault().Identifier);
            mockSerializer.Setup(x => x.Serialize(It.IsAny<Object>())).Returns(_serResult);

            var SUT = new RecordingMethodManager(mockCache.Object, mockThreadProvider.Object, mockStack.Object, mockSerializer.Object);
            SUT.MethodRecordingComplete += SUT_MethodRecordingComplete;
            SUT.ProcessCapture(TestClass.Method1Entry);

            Assert.IsTrue(_methodCompleteEventRaised == false);
        }

        [TestMethod]
        public void Recorded_Method_Manager_Handles_Recursion_Method_Adding()
        {
            var mockThreadProvider = new Mock<IThreadIdProvider>();
            var mockCache = new Mock<IExecutionCache>();
            var mockStack = new Mock<IExecutionStack>();
            var mockSerializer = new Mock<ISerializationHelper>();

            var threadId = 35;
            mockThreadProvider.Setup(x => x.GetThreadId()).Returns(threadId);

            var _methods = new List<RecordingMethod>() { new RecordingMethod(typeof(int), "", null, TestClass.Method1Entry.Method) };
            mockCache.Setup(x => x.GetMethods(It.IsAny<int>())).Returns(_methods);
            mockStack.Setup(x => x.ExecutingGuid(It.IsAny<int>())).Returns(_methods.FirstOrDefault().Identifier);
            mockSerializer.Setup(x => x.Serialize(It.IsAny<Object>())).Returns(_serResult);

            var SUT = new RecordingMethodManager(mockCache.Object, mockThreadProvider.Object, mockStack.Object, mockSerializer.Object);
            SUT.MethodRecordingComplete += SUT_MethodRecordingComplete;
            SUT.ProcessCapture(TestSubClass.Method1Entry);

            Assert.IsTrue(_methodCompleteEventRaised == false);
        }

        [TestMethod]
        public void Recorded_Method_Manager_Closes_Method_With_Stack_Depth_GT_1()
        {
            var mockThreadProvider = new Mock<IThreadIdProvider>();
            var mockCache = new Mock<IExecutionCache>();
            var mockStack = new Mock<IExecutionStack>();
            var mockSerializer = new Mock<ISerializationHelper>();

            var threadId = 35;
            mockThreadProvider.Setup(x => x.GetThreadId()).Returns(threadId);

            var _methods = new List<RecordingMethod>() { new RecordingMethod(typeof(int), "", null, TestClass.Method1Entry.Method),
                                                        new RecordingMethod(typeof(int), "", null, TestSubClass.Method1Entry.Method)};
            mockCache.Setup(x => x.GetMethods(It.IsAny<int>())).Returns(_methods);
            mockStack.Setup(x => x.ExecutingGuid(It.IsAny<int>())).Returns(_methods[1].Identifier);
            mockSerializer.Setup(x => x.Serialize(It.IsAny<Object>())).Returns(_serResult);

            var SUT = new RecordingMethodManager(mockCache.Object, mockThreadProvider.Object, mockStack.Object, mockSerializer.Object);
            SUT.MethodRecordingComplete += SUT_MethodRecordingComplete;
            SUT.ProcessCapture(TestSubClass.Method1Exit);

            Assert.IsTrue(_methodCompleteEventRaised == true);
        }

        [TestMethod]
        public void Recorded_Method_Manager_Verify_Completed_Method_Is_Returned()
        {
            var mockThreadProvider = new Mock<IThreadIdProvider>();
            var mockCache = new Mock<IExecutionCache>();
            var mockStack = new Mock<IExecutionStack>();
            var mockSerializer = new Mock<ISerializationHelper>();

            var threadId = 35;
            mockThreadProvider.Setup(x => x.GetThreadId()).Returns(threadId);

            var _methods = new List<RecordingMethod>() { new RecordingMethod(typeof(int), "", null, TestClass.Method1Entry.Method),
                                                        new RecordingMethod(typeof(int), "", null, TestSubClass.Method1Entry.Method)};
            mockCache.Setup(x => x.GetMethods(It.IsAny<int>())).Returns(_methods);
            mockStack.Setup(x => x.ExecutingGuid(It.IsAny<int>())).Returns(_methods[1].Identifier);
            mockSerializer.Setup(x => x.Serialize(It.IsAny<Object>())).Returns(_serResult);

            var SUT = new RecordingMethodManager(mockCache.Object, mockThreadProvider.Object, mockStack.Object, mockSerializer.Object);
            SUT.MethodRecordingComplete += SUT_MethodRecordingComplete;
            SUT.ProcessCapture(TestSubClass.Method1Exit);

            Assert.IsTrue(_eventArgs.Method.Identifier == _methods[1].Identifier);
        }

        [TestMethod]
        public void Recorded_Method_Manager_Verify_Execution_Stack_Is_Added_To()
        {
            var mockThreadProvider = new Mock<IThreadIdProvider>();
            var mockCache = new Mock<IExecutionCache>();
            var mockStack = new Mock<IExecutionStack>();
            var mockSerializer = new Mock<ISerializationHelper>();

            var threadId = 35;
            mockThreadProvider.Setup(x => x.GetThreadId()).Returns(threadId);

            var _methods = new List<RecordingMethod>() { new RecordingMethod(typeof(int), "", null, TestClass.Method1Entry.Method)};
            mockCache.Setup(x => x.GetMethods(It.IsAny<int>())).Returns(_methods);
            mockStack.Setup(x => x.ExecutingGuid(It.IsAny<int>())).Returns(_methods[1].Identifier);
            mockSerializer.Setup(x => x.Serialize(It.IsAny<Object>())).Returns(_serResult);

            var SUT = new RecordingMethodManager(mockCache.Object, mockThreadProvider.Object, mockStack.Object, mockSerializer.Object);
            SUT.ProcessCapture(TestSubClass.Method1Entry);

            mockStack.Verify(x => x.ProcessEntry(threadId, It.IsAny<Guid>()), Times.Once);
        }

        private void SUT_MethodRecordingComplete(object sender, MethodRecordingCompleteEventArgs e)
        {
            _methodCompleteEventRaised = true;
            _eventArgs = e;
        }
    }
}
