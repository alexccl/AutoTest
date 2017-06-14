using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder.ExecutionCache
{
    /// <summary>
    /// Provides the ID for the current executing thread
    /// </summary>
    interface IThreadIdProvider
    {
        /// <summary>
        /// Gets the managed thread ID
        /// </summary>
        /// <returns>thread ID</returns>
        int GetThreadId();
    }
}
