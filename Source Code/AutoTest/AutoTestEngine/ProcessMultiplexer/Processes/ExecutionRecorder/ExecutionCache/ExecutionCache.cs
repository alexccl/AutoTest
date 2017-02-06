using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder.ExecutionCache
{
    /// <summary>
    /// Cache for executing methods on different threads.  Threadsafe
    /// </summary>
    internal class ExecutionCache : IExecutionCache
    {
        private Object _lockObject = new object();
        private static Lazy<ConcurrentDictionary<int, List<RecordingMethod>>> _cache = new Lazy<ConcurrentDictionary<int, List<RecordingMethod>>>(() => new ConcurrentDictionary<int, List<RecordingMethod>>()); 

        public List<RecordingMethod> GetMethods(int threadId)
        {
            if(!_cache.Value.ContainsKey(threadId) )
            {
                 var newThreadMethods = new List<RecordingMethod>();
                 _cache.Value.TryAdd(threadId, newThreadMethods);
            }

            return _cache.Value[threadId];
        }

        public void ClearCache()
        {
            _cache.Value.Clear();
        }
    }
}
