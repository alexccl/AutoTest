using AutoTestEngine.ProcessMultiplexer.Processes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.ProcessMultiplexer
{
    internal class ProcessMultiplexer
    {
        private List<IProcess> _processes;
        public ProcessMultiplexer(IProcess[] processes)
        {
            _processes = processes.OrderBy(x => x.ProcessPriority).ToList();
        }

        public ProcessResult Process(InterceptionProcessingData processingData)
        {
            foreach(var process in _processes)
            {
                if(process.ShouldExecuteProcess(processingData))
                {
                    return process.ExecuteProcess(processingData);
                }
            }

            return new ProcessResult();
        }
    }
}
