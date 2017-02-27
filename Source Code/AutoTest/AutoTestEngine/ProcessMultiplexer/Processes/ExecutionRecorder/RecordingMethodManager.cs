using AutoTestEngine.Helpers;
using AutoTestEngine.Helpers.Serialization;
using AutoTestEngine.InterceptionVerification;
using AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder.ExecutionCache;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder
{
    /// <summary>
    /// Acts as a cache for methods currently being recorded.  Manages the entering and exiting of methods and raises event when method is done with execution and all execution metadata has been collected
    /// </summary>
    internal class RecordingMethodManager : IRecordingMethodManager
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

            switch (processingData.BoundaryType)
            {
                case BoundaryType.Entry:
                    ProcessEntry(processingData, methods, executingMethodId, threadId);
                    break;
                case BoundaryType.Exception:
                    ProcessException(processingData, methods, executingMethodId, threadId);
                    break;
                case BoundaryType.Exit:
                    ProcessExit(processingData, methods, executingMethodId, threadId);
                    break;
            }
        }

        private void ProcessEntry(InterceptionProcessingData data, List<RecordingMethod> methods, Guid executingMethodId, int threadId)
        {
            Guid newMethodId = Guid.NewGuid();
            //add this as a method
            if (!HasSerializationErrors(data))
            {
                var serInstance = _serializationHelper.Serialize(data.TargetInstance);

                if (!serInstance.Success) throw new AutoTestEngineException($"Unable to serialize type {data.TargetInstance.ToString()} despite not having a serialization failure on the processing datat context");



                var newMethod = new RecordingMethod(newMethodId, serInstance.SerializedValue, data.MethodArgs, data.Method);
                methods.Add(newMethod);
            }

            //add this as a sub-method if applicable
            var executingMethod = methods.FirstOrDefault(x => x.Identifier == executingMethodId);
            if (executingMethod != null)
            {
                var subMethod = new RecordedSubMethod(newMethodId, data.TargetType, data.MethodArgs, data.ReturnType, data.Method);
                executingMethod.SubMethods.Add(subMethod);
            }

            _executionStack.ProcessEntry(threadId, newMethodId);
        }

        private bool HasSerializationErrors(InterceptionProcessingData data)
        {
            return data.VerificationFailures.Any(x => x.FailureId.Equals(Failures.SerializationError));
        }

        private void ProcessExit(InterceptionProcessingData data, List<RecordingMethod> methods, Guid executingMethodId, int threadId)
        {
            var subMethod = GetSubMethod(executingMethodId, methods);
            var hasError = HasSerializationErrors(data);

            if(subMethod != null)
            {
                if (hasError)
                {
                    ClearMethodFromSubId(executingMethodId, threadId);
                }
                else
                {
                    subMethod.CloseOutMethodWithReturnVal(data.ReturnValue);
                }
            }

            var executingMethod = methods.FirstOrDefault(x => x.Identifier == executingMethodId);
            if (executingMethod != null)
            {
                if (!hasError)
                {
                    executingMethod.CloseOutMethodWithReturnVal(data.ReturnValue.Value);
                    OnMethodRecordingComplete(new MethodRecordingCompleteEventArgs(executingMethod));
                    ClearMethod(executingMethodId, threadId);
                }
                else
                {
                    ClearMethod(executingMethodId, threadId);
                }
            }

            _executionStack.ProcessExit(threadId);
        }

        private void ClearMethod(Guid methodId, int threadId)
        {
            var methods = _executionCache.GetMethods(threadId);
            methods.RemoveAll(x => x.Identifier == methodId);
        }

        private void ClearMethodFromSubId(Guid subMethodId, int threadId)
        {
            var methods = _executionCache.GetMethods(threadId);
            var methodId = GetMethodFromSubId(subMethodId, methods).Identifier;
            ClearMethod(methodId, threadId);
        }

        private RecordingMethod GetMethodFromSubId(Guid subMethodId, List<RecordingMethod> methods)
        {
            return methods.FirstOrDefault(x => x.SubMethods.Any(y => y.Identifier == subMethodId));
        }

        private RecordedSubMethod GetSubMethod(Guid subMethodId, List<RecordingMethod> methods)
        {
            var method = GetMethodFromSubId(subMethodId, methods);

            if (method == null) return null;

            return method.SubMethods.FirstOrDefault(x => x.Identifier == subMethodId);
        }


        private void ProcessException(InterceptionProcessingData data, List<RecordingMethod> methods, Guid executingMethodId, int threadId)
        {
            var subMethod = GetSubMethod(executingMethodId, methods);

            if (subMethod != null)
            {
                subMethod.CloseOutMethodWithException(data.Exception);
            }

            var executingMethod = methods.FirstOrDefault(x => x.Identifier == executingMethodId);
            if (executingMethod != null)
            {

                executingMethod.CloseOutMethodWithException(data.Exception);
                OnMethodRecordingComplete(new MethodRecordingCompleteEventArgs(executingMethod));
                ClearMethod(executingMethodId, threadId);
            }

            _executionStack.ProcessException(threadId);
        }




    }

    internal class MethodRecordingCompleteEventArgs : EventArgs
    {
        public RecordingMethod Method { get; set; }

        public MethodRecordingCompleteEventArgs(RecordingMethod method)
        {
            this.Method = method;
        }
    }
}
