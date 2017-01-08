using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine
{
    internal class InterceptionProcessingData
    {
        public BoundaryType BoundaryType { get; private set; }
        public object TargetInstance { get; internal set; }
        public object ReturnValue { get; set; }
        public Exception Exception { get; set; }
        public List<object> MethodArgs { get; set; }
        public MethodBase Method { get; set; }
        public EngineConfiguration Configuration { get; internal set; }
        public List<Type> ClassAttributes { get; private set; }
        public List<Type> MethodAttributes { get; private set; }


        public Type ReturnType { get
            {
                if (ReturnValue != null) return ReturnValue.GetType();

                if (Method != null) return ((MethodInfo)this.Method).ReturnType;

                else throw new Exception("Unable to find type info on current instance of ProcessingData class");
            } }

        public InterceptionProcessingData(InterceptionEntryModel entryModel, EngineConfiguration configuration)
        {
            this.BoundaryType = BoundaryType.Entry;
            this.TargetInstance = entryModel.TargetInstance;
            this.MethodArgs = entryModel.MethodArgs;
            this.Method = entryModel.Method;
            this.Configuration = configuration;

            GetAttributesFromMethodBase(entryModel.Method);
        }

        public InterceptionProcessingData(InterceptionExceptionModel exceptionModel, EngineConfiguration configuration)
        {
            this.BoundaryType = BoundaryType.Exception;
            this.TargetInstance = exceptionModel.TargetInstance;
            this.Exception = exceptionModel.Exception;
            this.Method = exceptionModel.Method;
            this.Configuration = configuration;

            GetAttributesFromMethodBase(exceptionModel.Method);
        }

        public InterceptionProcessingData(InterceptionExitModel exitModel, EngineConfiguration configuration)
        {
            this.BoundaryType = BoundaryType.Exit;
            this.TargetInstance = this.TargetInstance;
            this.ReturnValue = exitModel.ReturnValue;
            this.Method = exitModel.Method;
            this.Configuration = configuration;

            GetAttributesFromMethodBase(exitModel.Method);
        }

        private void GetAttributesFromMethodBase(MethodBase method)
        {
            var autoTestNamespace = typeof(AutoTestEngine.Attributes.AutoTest).Namespace;
            this.MethodAttributes = method.CustomAttributes
                                          .Where(x => x.AttributeType.Namespace != null)
                                          .Where(x => x.AttributeType.Namespace.Equals(autoTestNamespace))
                                          .Select(x => x.AttributeType)
                                          .ToList();

            this.ClassAttributes = method.DeclaringType.CustomAttributes
                                          .Where(x => x.AttributeType.Namespace != null)
                                          .Where(x => x.AttributeType.Namespace.Equals(autoTestNamespace))
                                          .Select(x => x.AttributeType)
                                          .ToList();
        }
    }
}
