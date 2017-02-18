using AutoTestEngine.Helpers.Serialization;
using AutoTestEngine.InterceptionVerification.VerificationResult;
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

        private SerializationResult _sucSerResult = SerializationResult.InitSuccessfulSerialization(null, "serialized object");

        private Mock<IThreadIdProvider> _defaultThreadProvider;
        private Mock<ISerializationHelper> _defaultSerializer;

        private int _defaultThreadId = 35;

        private Guid _emptySentinel = new Guid("4FFD2F9C-13AE-42D8-810C-C7085A856B35");

        [TestInitialize]
        public void Setup()
        {
            var mockThreadProvider = new Mock<IThreadIdProvider>();
            mockThreadProvider.Setup(x => x.GetThreadId()).Returns(_defaultThreadId);
            _defaultThreadProvider = mockThreadProvider;

            var mockSerializer = new Mock<ISerializationHelper>();
            mockSerializer.Setup(x => x.Serialize(It.IsAny<Object>())).Returns(_sucSerResult);
            _defaultSerializer = mockSerializer;
        }

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
                    new RecordingMethod(Guid.NewGuid(), typeof(int), "", null, TestClass.Method1Entry.Method)
                };

            }
        }

        [TestMethod]
        public void Recorded_Method_Manager_Test_Thread_Id_Is_Used_On_Execution_Cache()
        {
            var mockCache = new Mock<IExecutionCache>();
            var mockStack = new Mock<IExecutionStack>();

            var SUT = new RecordingMethodManager(mockCache.Object, _defaultThreadProvider.Object, mockStack.Object, _defaultSerializer.Object);

            SUT.ProcessCapture(TestClass.Method1Entry);
            mockCache.Verify(x => x.GetMethods(_defaultThreadId), Times.Once);
        }

        [TestMethod]
        public void Recorded_Method_Manager_Test_Thread_Id_Is_Used_On_Execution_Stack()
        {
            var mockCache = new Mock<IExecutionCache>();
            var mockStack = new Mock<IExecutionStack>();

            var SUT = new RecordingMethodManager(mockCache.Object, _defaultThreadProvider.Object, mockStack.Object, _defaultSerializer.Object);

            SUT.ProcessCapture(TestClass.Method1Entry);
            mockStack.Verify(x => x.ProcessEntry(_defaultThreadId, It.IsAny<Guid>()), Times.Once);
        }

        [TestMethod]
        public void Recorded_Method_Manager_Method_Exit_Raises_Complete_Event()
        {
            var mockCache = new Mock<IExecutionCache>();
            var mockStack = new Mock<IExecutionStack>();


            var _methods = new List<RecordingMethod>() { new RecordingMethod(Guid.NewGuid(), typeof(int), "", null, TestClass.Method1Entry.Method) };
            mockCache.Setup(x => x.GetMethods(It.IsAny<int>())).Returns(_methods);
            mockStack.Setup(x => x.ExecutingGuid(It.IsAny<int>())).Returns(_methods.FirstOrDefault().Identifier);

            var SUT = new RecordingMethodManager(mockCache.Object, _defaultThreadProvider.Object, mockStack.Object, _defaultSerializer.Object);
            SUT.MethodRecordingComplete += SUT_MethodRecordingComplete;
            SUT.ProcessCapture(TestClass.Method1Exit);

            Assert.IsTrue(_methodCompleteEventRaised == true);
        }

        [TestMethod]
        public void Recorded_Method_Manager_Method_Exception_Raises_Complete_Event()
        {
            var mockCache = new Mock<IExecutionCache>();
            var mockStack = new Mock<IExecutionStack>();

            var _methods = new List<RecordingMethod>() { new RecordingMethod(Guid.NewGuid(), typeof(int), "", null, TestClass.Method1Entry.Method) };
            mockCache.Setup(x => x.GetMethods(It.IsAny<int>())).Returns(_methods);
            mockStack.Setup(x => x.ExecutingGuid(It.IsAny<int>())).Returns(_methods.FirstOrDefault().Identifier);

            var SUT = new RecordingMethodManager(mockCache.Object, _defaultThreadProvider.Object, mockStack.Object, _defaultSerializer.Object);
            SUT.MethodRecordingComplete += SUT_MethodRecordingComplete;
            SUT.ProcessCapture(TestClass.Method1Exception);

            Assert.IsTrue(_methodCompleteEventRaised == true);
        }

        [TestMethod]
        public void Recorded_Method_Manager_Method_Entry_Wont_Raise_Complete_Event()
        {
            var mockCache = new Mock<IExecutionCache>();
            var mockStack = new Mock<IExecutionStack>();

            var _methods = new List<RecordingMethod>() { new RecordingMethod(Guid.NewGuid(), typeof(int), "", null, TestClass.Method1Entry.Method) };
            mockCache.Setup(x => x.GetMethods(It.IsAny<int>())).Returns(_methods);
            mockStack.Setup(x => x.ExecutingGuid(It.IsAny<int>())).Returns(_methods.FirstOrDefault().Identifier);

            var SUT = new RecordingMethodManager(mockCache.Object, _defaultThreadProvider.Object, mockStack.Object, _defaultSerializer.Object);
            SUT.MethodRecordingComplete += SUT_MethodRecordingComplete;
            SUT.ProcessCapture(TestClass.Method1Entry);

            Assert.IsTrue(_methodCompleteEventRaised == false);
        }

        [TestMethod]
        public void Recorded_Method_Manager_Handles_Recursion_Method_Adding()
        {
            var mockCache = new Mock<IExecutionCache>();
            var mockStack = new Mock<IExecutionStack>();

            var _methods = new List<RecordingMethod>() { new RecordingMethod(Guid.NewGuid(), typeof(int), "", null, TestClass.Method1Entry.Method) };
            mockCache.Setup(x => x.GetMethods(It.IsAny<int>())).Returns(_methods);
            mockStack.Setup(x => x.ExecutingGuid(It.IsAny<int>())).Returns(_methods.FirstOrDefault().Identifier);

            var SUT = new RecordingMethodManager(mockCache.Object, _defaultThreadProvider.Object, mockStack.Object, _defaultSerializer.Object);
            SUT.MethodRecordingComplete += SUT_MethodRecordingComplete;
            SUT.ProcessCapture(TestSubClass.Method1Entry);

            Assert.IsTrue(_methodCompleteEventRaised == false);
        }

        [TestMethod]
        public void Recorded_Method_Manager_Closes_Method_With_Stack_Depth_GT_1()
        {
            var mockCache = new Mock<IExecutionCache>();
            var mockStack = new Mock<IExecutionStack>();

            var _methods = new List<RecordingMethod>() { new RecordingMethod(Guid.NewGuid(), typeof(int), "", null, TestClass.Method1Entry.Method),
                                                        new RecordingMethod(Guid.NewGuid(), typeof(int), "", null, TestSubClass.Method1Entry.Method)};
            mockCache.Setup(x => x.GetMethods(It.IsAny<int>())).Returns(_methods);
            mockStack.Setup(x => x.ExecutingGuid(It.IsAny<int>())).Returns(_methods[1].Identifier);

            var SUT = new RecordingMethodManager(mockCache.Object, _defaultThreadProvider.Object, mockStack.Object, _defaultSerializer.Object);
            SUT.MethodRecordingComplete += SUT_MethodRecordingComplete;
            SUT.ProcessCapture(TestSubClass.Method1Exit);

            Assert.IsTrue(_methodCompleteEventRaised == true);
        }

        [TestMethod]
        public void Recorded_Method_Manager_Verify_Completed_Method_Is_Returned()
        {
            var mockCache = new Mock<IExecutionCache>();
            var mockStack = new Mock<IExecutionStack>();

            var _methods = new List<RecordingMethod>() { new RecordingMethod(Guid.NewGuid(), typeof(int), "", null, TestClass.Method1Entry.Method),
                                                        new RecordingMethod(Guid.NewGuid(), typeof(int), "", null, TestSubClass.Method1Entry.Method)};
            mockCache.Setup(x => x.GetMethods(It.IsAny<int>())).Returns(_methods);
            mockStack.Setup(x => x.ExecutingGuid(It.IsAny<int>())).Returns(_methods[1].Identifier);

            var SUT = new RecordingMethodManager(mockCache.Object, _defaultThreadProvider.Object, mockStack.Object, _defaultSerializer.Object);
            SUT.MethodRecordingComplete += SUT_MethodRecordingComplete;
            SUT.ProcessCapture(TestSubClass.Method1Exit);

            Assert.IsTrue(_eventArgs.Method.Identifier == _methods[1].Identifier);
        }

        [TestMethod]
        public void Recorded_Method_Manager_Verify_Execution_Stack_Is_Added_To()
        {
            var mockCache = new Mock<IExecutionCache>();
            var mockStack = new Mock<IExecutionStack>();

            var _methods = new List<RecordingMethod>() { new RecordingMethod(Guid.NewGuid(), typeof(int), "", null, TestClass.Method1Entry.Method)};
            mockCache.Setup(x => x.GetMethods(It.IsAny<int>())).Returns(_methods);
            mockStack.Setup(x => x.ExecutingGuid(It.IsAny<int>())).Returns(_methods[1].Identifier);

            var SUT = new RecordingMethodManager(mockCache.Object, _defaultThreadProvider.Object, mockStack.Object, _defaultSerializer.Object);
            SUT.ProcessCapture(TestSubClass.Method1Entry);

            mockStack.Verify(x => x.ProcessEntry(_defaultThreadId, It.IsAny<Guid>()), Times.Once);
        }

        [TestMethod]
        public void Recorded_Method_Manager_Instace_Verifier_Fail_Should_Not_Call_Serializer()
        {
            var mockCache = new Mock<IExecutionCache>();
            var mockStack = new Mock<IExecutionStack>();

            var _methods = new List<RecordingMethod>();
            mockCache.Setup(x => x.GetMethods(It.IsAny<int>())).Returns(_methods);

            var SUT = new RecordingMethodManager(mockCache.Object, _defaultThreadProvider.Object, mockStack.Object, _defaultSerializer.Object);

            var procData = TestClass.Method1Entry;
            procData.AddVerificationFailures(new List<VerificationFailure>() { new TypeSerializationFailure(typeof(double)) });
            SUT.ProcessCapture(procData);

            _defaultSerializer.Verify(x => x.Serialize(It.IsAny<object>()), Times.Never);
        }

        [TestMethod]
        public void Recorded_Method_Manager_Return_Val_Verifier_Fail_Should_Not_Call_Serializer()
        {
            var mockCache = new Mock<IExecutionCache>();
            var mockStack = new Mock<IExecutionStack>();

            var _methods = new List<RecordingMethod>();
            mockCache.Setup(x => x.GetMethods(It.IsAny<int>())).Returns(_methods);
            mockStack.Setup(x => x.ExecutingGuid(It.IsAny<int>())).Returns(_emptySentinel);

            var SUT = new RecordingMethodManager(mockCache.Object, _defaultThreadProvider.Object, mockStack.Object, _defaultSerializer.Object);

            var procData = TestClass.Method1Exit;
            procData.AddVerificationFailures(new List<VerificationFailure>() { new TypeSerializationFailure(typeof(double)) });
            SUT.ProcessCapture(procData);

            _defaultSerializer.Verify(x => x.Serialize(It.IsAny<object>()), Times.Never);
        }

        [TestMethod]
        public void Recorded_Method_Manager_Instance_Verifier_Fail_Should_Not_Create_New_Exec_Method()
        {
            var mockCache = new Mock<IExecutionCache>();
            var mockStack = new Mock<IExecutionStack>();

            var _methods = new List<RecordingMethod>();
            mockCache.Setup(x => x.GetMethods(It.IsAny<int>())).Returns(_methods);
            mockStack.Setup(x => x.ExecutingGuid(It.IsAny<int>())).Returns(_emptySentinel);

            var SUT = new RecordingMethodManager(mockCache.Object, _defaultThreadProvider.Object, mockStack.Object, _defaultSerializer.Object);

            var procData = TestClass.Method1Exit;
            procData.AddVerificationFailures(new List<VerificationFailure>() { new TypeSerializationFailure(typeof(double)) });
            SUT.ProcessCapture(procData);

            Assert.IsTrue(_methods.Count == 0);
        }

        [TestMethod]
        public void Recorded_Method_Manager_Instance_Verifier_Fail_Should_Still_Add_Sub_Method()
        {
            var mockCache = new Mock<IExecutionCache>();
            var mockStack = new Mock<IExecutionStack>();

            var _methods = new List<RecordingMethod>();
            mockCache.Setup(x => x.GetMethods(It.IsAny<int>())).Returns(new List<RecordingMethod>() { new RecordingMethod(Guid.NewGuid(), typeof(int), "", null, TestClass.Method1Entry.Method) });

            mockStack.Setup(x => x.ExecutingGuid(It.IsAny<int>())).Returns(_emptySentinel);

            var SUT = new RecordingMethodManager(mockCache.Object, _defaultThreadProvider.Object, mockStack.Object, _defaultSerializer.Object);

            var procData = TestSubClass.Method1Entry;
            procData.AddVerificationFailures(new List<VerificationFailure>() { new TypeSerializationFailure(typeof(double)) });
            SUT.ProcessCapture(procData);

            Assert.IsTrue(_methods.Count == 1);
            Assert.IsTrue(_methods[0].SubMethods.Count == 1);
        }

        [TestMethod]
        public void Recorded_Method_Manager_Sub_Method_With_Invalid_Return_Should_Clear_From_Cache()
        {
            var mockCache = new Mock<IExecutionCache>();
            var mockStack = new Mock<IExecutionStack>();

            var _methods = new List<RecordingMethod>();

            
            var recMethod = new RecordingMethod(Guid.NewGuid(), typeof(int), "", null, TestClass.Method1Entry.Method);
            var subMethodGuid = Guid.NewGuid();
            recMethod.SubMethods.Add(new RecordedSubMethod(subMethodGuid, typeof(int), new List<TypeValModel>(), typeof(int), "blah"));

            mockCache.Setup(x => x.GetMethods(It.IsAny<int>())).Returns(new List<RecordingMethod>() { recMethod });
            mockStack.Setup(x => x.ExecutingGuid(It.IsAny<int>())).Returns(subMethodGuid);

            var SUT = new RecordingMethodManager(mockCache.Object, _defaultThreadProvider.Object, mockStack.Object, _defaultSerializer.Object);

            var procData = TestSubClass.Method1Exit;
            procData.AddVerificationFailures(new List<VerificationFailure>() { new TypeSerializationFailure(typeof(double)) });
            SUT.ProcessCapture(procData);

            Assert.IsTrue(_methods.Count == 0);
        }

        private void SUT_MethodRecordingComplete(object sender, MethodRecordingCompleteEventArgs e)
        {
            _methodCompleteEventRaised = true;
            _eventArgs = e;
        }
    }
}
