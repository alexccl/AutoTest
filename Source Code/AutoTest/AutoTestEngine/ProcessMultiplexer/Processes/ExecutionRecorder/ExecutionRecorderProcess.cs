using AutoTestEngine.DAL;
using AutoTestEngine.DAL.Helpers;
using AutoTestEngine.DAL.Models;
using AutoTestEngine.DAL.TexFileImplementation;
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
        private IRecordedMethodHelper _dataHelper;

        //TODO: add new dal helper
        public ExecutionRecorderProcess(IRecordingMethodManager recordingMethodManager, IRecordedMethodHelper dataHelper)
        {
            _methodManager = recordingMethodManager;
            _methodManager.MethodRecordingComplete += _methodManager_MethodRecordingComplete;

            _dataHelper = dataHelper;
        }

        private void _methodManager_MethodRecordingComplete(object sender, MethodRecordingCompleteEventArgs e)
        {
            var recordingMethod = e.Method;
            var recordedMethod = new RecordedMethod(recordingMethod);
            _dataHelper.AddRecordedMethod(recordedMethod);
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
            _methodManager.ProcessCapture(processingData);
            return new ProcessResult();

            var x = Repository.StoredObject;
        }

        public bool ShouldExecuteProcess(InterceptionProcessingData processingData)
        {
            return !processingData.Configuration.IsUnitTesting;
        }
    }
}
