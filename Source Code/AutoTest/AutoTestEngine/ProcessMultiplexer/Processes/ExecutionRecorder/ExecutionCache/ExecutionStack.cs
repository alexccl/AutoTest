using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder.ExecutionCache
{
    /// <summary>
    /// Manages the stack of method guids in a thread safe way
    /// </summary>
    internal class ExecutionStack : IExecutionStack
    {
        private Lazy<ConcurrentDictionary<int, Stack<Guid>>> _threadStacks = new Lazy<ConcurrentDictionary<int, Stack<Guid>>>(() => new ConcurrentDictionary<int, Stack<Guid>>());

        private Stack<Guid> GetThreadStack(int threadId)
        {
            if(_threadStacks.Value.ContainsKey(threadId))
            {
                return _threadStacks.Value[threadId];
            }

            var newStack = new Stack<Guid>();
            if(!_threadStacks.Value.TryAdd(threadId, newStack))
            {
                newStack = _threadStacks.Value[threadId];
            }

            return newStack;
        }



        public Guid EmptyStackSentinel
        {
            get
            {
                return new Guid("4FFD2F9C-13AE-42D8-810C-C7085A856B35");
            }
        }

        public Guid ExecutingGuid(int threadId)
        {
            var stack = GetThreadStack(threadId);

            if (stack.Count == 0) return this.EmptyStackSentinel;

            return stack.Peek();
        }

        public bool IsStackEmpty(int threadId)
        {
            return (this.ExecutingGuid(threadId) == this.EmptyStackSentinel);
        }

        public void ClearStack()
        {
            _threadStacks.Value.Clear();
        }

        public void ProcessEntry(int threadId, Guid newMethodGuid)
        {
            GetThreadStack(threadId).Push(newMethodGuid);
        }

        public void ProcessException(int threadId)
        {
            this.ProcessExit(threadId);
        }

        public void ProcessExit(int threadId)
        {
            if (this.IsStackEmpty(threadId)) throw new ExecutionStackEmptyException();

            GetThreadStack(threadId).Pop();
        }
    }
}
