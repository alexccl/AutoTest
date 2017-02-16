using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine
{
    /// <summary>
    /// Container for all interception data, this is what is processed in teh engine pipeline
    /// </summary>
    internal class InterceptionProcessingData
    {
        /// <summary>
        /// Determines the boundary the interception occured on
        /// </summary>
        public BoundaryType BoundaryType { get; private set; }

        /// <summary>
        /// The instance of the the intercepted class at the time of invocation
        /// </summary>
        public object TargetInstance { get; internal set; }

        public Type TargetType
        {
            get
            {
                return TargetInstance.GetType();
            }
        }

        /// <summary>
        /// Return value of the invocation, can be null based on boundary context
        /// </summary>
        public object ReturnValue { get; set; }

        /// <summary>
        /// Exception thrown by invocation, can be null based on boundary context
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Arguments passed to the method
        /// </summary>
        public List<object> MethodArgs { get; set; }

        /// <summary>
        /// Method metadata of called method
        /// </summary>
        public MethodBase Method { get; set; }
        public EngineConfiguration Configuration { get; internal set; }
        public List<Type> ClassAttributes { get; private set; }
        public List<Type> MethodAttributes { get; private set; }

        /// <summary>
        /// Contains all the verifiers this capture has failed
        /// </summary>
        public List<Guid> VerificationFailures { get; private set; }


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
            this.TargetInstance = exitModel.TargetInstance;
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
