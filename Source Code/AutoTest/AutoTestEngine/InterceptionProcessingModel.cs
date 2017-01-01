using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine
{
    internal class InterceptionProcessingModel
    {
        public BoundaryType BoundaryType { get; private set; }
        public object TargetInstance { get; internal set; }
        public object ReturnValue { get; set; }
        public Exception Exception { get; set; }
        public List<object> MethodArgs { get; set; }
        public MethodBase Method { get; set; }
        public EngineConfiguration Configuration { get; internal set; }

        public Type ReturnType { get
            {
                if (ReturnValue != null) return ReturnValue.GetType();

                if (Method != null) return ((MethodInfo)this.Method).ReturnType;

                else throw new Exception("Unable to find type info on current instance of ProcessingData class");
            } }

        public InterceptionProcessingModel(InterceptionEntryModel entryModel, EngineConfiguration configuration)
        {
            this.BoundaryType = BoundaryType.Entry;
            this.TargetInstance = entryModel.TargetInstance;
            this.MethodArgs = entryModel.MethodArgs;
            this.Method = entryModel.Method;
            this.Configuration = configuration;
        }

        public InterceptionProcessingModel(InterceptionExceptionModel exceptionModel, EngineConfiguration configuration)
        {
            this.BoundaryType = BoundaryType.Exception;
            this.TargetInstance = exceptionModel.TargetInstance;
            this.Exception = exceptionModel.Exception;
            this.Method = exceptionModel.Method;
            this.Configuration = configuration;
        }

        public InterceptionProcessingModel(InterceptionExitModel exitModel, EngineConfiguration configuration)
        {
            this.BoundaryType = BoundaryType.Exit;
            this.TargetInstance = this.TargetInstance;
            this.ReturnValue = exitModel.ReturnValue;
            this.Method = exitModel.Method;
            this.Configuration = configuration;
        }
    }
}
