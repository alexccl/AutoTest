using AutoTestEngine.Helpers.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder
{
    internal class RecordingMethod
    {
        public Guid Identifier { get; private set; }
        public bool IsExecutionComplete { get; private set; }
        public SerializedValue InstanceAtExecutionTime { get; private set; }
        public TypeValModel ReturnTypeVal { get; set; }
        public Type TargetType { get; private set; }
        public List<TypeValModel> Args { get; set; }
        public List<RecordedSubMethod> SubMethods { get; set; }
        public Exception MethodException { get; private set; }
        public string MethodName { get; private set; }
        public MethodBase Method { get; private set; }

        public RecordingMethod(Guid id, SerializedValue serializedTarget, List<TypeValModel> args, MethodBase method)
        {
            this.Identifier = id;
            this.TargetType = serializedTarget.Type;
            this.InstanceAtExecutionTime = serializedTarget;
            this.SubMethods = new List<RecordedSubMethod>();
            this.MethodName = method.Name;
            this.Method = method;

            this.Args = args;

            this.ReturnTypeVal = new TypeValModel() { Type = ((MethodInfo)method).ReturnType };
        }

        public void CloseOutMethodWithReturnVal(Object returnVal)
        {
            this.ReturnTypeVal.Value = returnVal;
            this.IsExecutionComplete = true;
        }

        public void CloseOutMethodWithException(Exception ex)
        {
            this.IsExecutionComplete = true;
            this.MethodException = ex;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is RecordingMethod)) return false;

            var method2 = (RecordingMethod)obj;
            return (method2.Identifier.Equals(this.Identifier));
        }

    }
}
