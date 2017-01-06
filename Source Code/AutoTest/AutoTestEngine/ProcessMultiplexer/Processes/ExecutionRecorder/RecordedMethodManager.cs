using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder
{
    internal class RecordedMethodManager
    {
        public List<RecordedMethod> RecordingMethods { get; private set; }

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
