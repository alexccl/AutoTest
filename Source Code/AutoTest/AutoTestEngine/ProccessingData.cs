using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine
{
    internal class ProccessingData
    {
        public BoundaryType BoundaryType { get; private set; }
        public object TargetInstance { get; internal set; }
        public object ReturnValue { get; set; }
        public Exception Exception { get; set; }
        public List<object> MethodArgs { get; set; }
        public MethodBase Method { get; set; }

        public Type ReturnType { get
            {
                if (ReturnValue != null) return ReturnValue.GetType();

                if (Method != null) return ((MethodInfo)this.Method).ReturnType;

                else throw new Exception("Unable to find type info on current instance of ProcessingData class");
            } }

        public ProccessingData(InterceptionEntryModel entryModel)
        {
            this.BoundaryType = BoundaryType.Entry;
            this.TargetInstance = entryModel.TargetInstance;
            this.MethodArgs = entryModel.MethodArgs;
            this.Method = entryModel.Method;
        }

        public ProccessingData(InterceptionExceptionModel exceptionModel)
        {
            this.BoundaryType = BoundaryType.Exception;
            this.TargetInstance = exceptionModel.TargetInstance;
            this.Exception = exceptionModel.Exception;
            this.Method = exceptionModel.Method;
        }

        public ProccessingData(InterceptionExitModel exitModel)
        {
            this.BoundaryType = BoundaryType.Exit;
            this.TargetInstance = this.TargetInstance;
            this.ReturnValue = exitModel.ReturnValue;
            this.Method = exitModel.Method;
        }
    }
}
