using AutoTestEngine.ProcessMultiplexer.Processes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.ProcessMultiplexer
{
    /// <summary>
    /// Determines which process the interception should perform and executes that process
    /// </summary>
    internal class ProcessMultiplexer : IProcessMultiplexer
    {
        private List<IProcess> _processes;
        public ProcessMultiplexer(IProcess[] processes)
        {
            _processes = processes.OrderBy(x => x.ProcessPriority).ToList();
        }

        /// <summary>
        /// Process the interception data
        /// </summary>
        /// <param name="processingData">data from interception</param>
        /// <returns>Result of process</returns>
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
