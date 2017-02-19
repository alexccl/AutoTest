using AutoTestEngine.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder
{
    internal class ExecutionRecorderProcess : IProcess
    {
        private IRecordingMethodManager _methodManager;

        //TODO: add new dal helper
        public ExecutionRecorderProcess(IRecordingMethodManager recordingMethodManager)
        {

        }
        public int ProcessPriority
        {
            get
            {
                return 2;
            }
        }

        public ProcessResult ExecuteProcess(InterceptionProcessingData processingData)
        {
            //_methodManager.ProcessCapture();
            throw new NotImplementedException();
        }

        public bool ShouldExecuteProcess(InterceptionProcessingData processingData)
        {
            throw new NotImplementedException();
        }
    }
}
