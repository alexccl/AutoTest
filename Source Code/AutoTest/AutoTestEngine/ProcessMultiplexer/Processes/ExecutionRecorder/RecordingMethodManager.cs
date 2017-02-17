using AutoTestEngine.Helpers;
using AutoTestEngine.Helpers.Serialization;
using AutoTestEngine.InterceptionVerification;
using AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder.ExecutionCache;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder
{
    /// <summary>
    /// Acts as a cache for methods currently being recorded.  Manages the entering and exiting of methods and raises event when method is done with execution and all execution metadata has been collected
    /// </summary>
    internal class RecordingMethodManager
    {
        /// <summary>
        /// Stores the currently recorded methods in temp memory
        /// </summary>
        private IExecutionCache _executionCache;

        /// <summary>
        /// Provides the ID of the current thread
        /// </summary>
        private IThreadIdProvider _threadProvider;

        /// <summary>
        /// Keeps track of the stack of executing method's unique identifiers
        /// </summary>
        private IExecutionStack _executionStack;

        /// <summary>
        /// Performs serializations
        /// </summary>
        private ISerializationHelper _serializationHelper;

        public RecordingMethodManager(IExecutionCache executionCache, IThreadIdProvider threadProvider, IExecutionStack executionStack, ISerializationHelper serializationHelper)
        {
            _executionCache = executionCache;
            _threadProvider = threadProvider;
            _executionStack = executionStack;
            _serializationHelper = serializationHelper;
        }

        /// <summary>
        /// Collection of all the methods that are currently being recorded
        /// </summary>
        private List<RecordingMethod> RecordingMethods { get; set; }

        /// <summary>
        /// Fires when a recording is complete.  Recorded method passed as event arg.  Manager releases all method recording resources after firing
        /// </summary>
        public event EventHandler<MethodRecordingCompleteEventArgs> MethodRecordingComplete;

        protected virtual void OnMethodRecordingComplete(MethodRecordingCompleteEventArgs e)
        {
            EventHandler<MethodRecordingCompleteEventArgs> handler = MethodRecordingComplete;
            if (handler != null) handler(this, e);
        }

        public void ProcessCapture(InterceptionProcessingData processingData)
        {
            var threadId = _threadProvider.GetThreadId();
            var methods = _executionCache.GetMethods(threadId);
            var executingMethodId = _executionStack.ExecutingGuid(threadId);
        }

        private void ProcessEntry(InterceptionProcessingData data, List<RecordingMethod> methods, Guid executingMethodId)
        {
            //add this as a sub-method if applicable
            var executingMethod = methods.FirstOrDefault(x => x.Identifier == executingMethodId);
            if(executingMethod != null)
            {
                var subMethodId = Guid.NewGuid();
                var subMethod = new RecordedSubMethod(subMethodId, data.TargetType, data.MethodArgs.ToTypeValList(), data.ReturnType, data.Method.Name);
                executingMethod.SubMethods.Add(subMethod);
            }

            //add this as a method
            //var newExecutingMethod = new RecordedMethod(data.TargetType, data.S)
        }

        private bool ValidateEntryForNewExecutingMethod(InterceptionProcessingData data)
        {
            return !data.VerificationFailures.Any(x => x.FailureId.Equals(Failures.SerializationError));
        }

        private void ProcessExit(InterceptionProcessingData data, List<RecordingMethod> methods, Guid executingMethodId)
        {

        }
        private void ProcessInterception(InterceptionProcessingData data, List<RecordingMethod> methods, Guid executingMethodId)
        {

        }




    }

    internal class MethodRecordingCompleteEventArgs : EventArgs
    {
        public RecordingMethod Method { get; set; }
    }
}
