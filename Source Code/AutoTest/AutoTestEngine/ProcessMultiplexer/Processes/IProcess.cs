using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.ProcessMultiplexer.Processes
{
    internal interface IProcess
    {
        int ProcessPriority { get; }
        bool ShouldExecuteProcess(InterceptionProcessingData processingData);
        ProcessResult ExecuteProcess(InterceptionProcessingData processingData);
    }
}
