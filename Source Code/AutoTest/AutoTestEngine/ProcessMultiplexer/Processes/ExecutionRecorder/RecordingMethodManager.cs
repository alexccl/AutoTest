using AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder.ExecutionCache;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder
{
    /// <summary>
    /// Acts as a cache for methods currently being recorded.
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

        public RecordingMethodManager(IExecutionCache executionCache, IThreadIdProvider threadProvider, IExecutionStack executionStack)
        {
            _executionCache = executionCache;
            _threadProvider = threadProvider;
            _executionStack = executionStack;
        }

        /// <summary>
        /// Collection of all the methods that are currently being recorded
        /// </summary>
        private List<RecordedMethod> RecordingMethods { get; set; }

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

        }

         
    }

    internal class MethodRecordingCompleteEventArgs : EventArgs
    {
        public RecordedMethod Method { get; set; }
    }
}
