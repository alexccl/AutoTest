using System;
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
        private static Lazy<Dictionary<int, List<RecordedMethod>>> _cache = new Lazy<Dictionary<int, List<RecordedMethod>>>(() => new Dictionary<int, List<RecordedMethod>>()); 

        public List<RecordedMethod> GetMethods(int threadId)
        {
            lock (_lockObject)
            {
               if(!_cache.Value.ContainsKey(threadId) )
               {
                    var newThreadMethods = new List<RecordedMethod>();
                    _cache.Value.Add(threadId, newThreadMethods);
               }

                return _cache.Value[threadId];
            }
        }

        public void ClearCache()
        {
            _cache.Value.Clear();
        }
    }
}
