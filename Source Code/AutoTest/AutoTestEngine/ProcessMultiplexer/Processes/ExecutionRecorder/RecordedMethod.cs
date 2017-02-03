using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder
{
    internal class RecordedMethod
    {
        public Guid Identifier { get; private set; }
        public bool ExecutionComplete { get; private set; }
        public string InstanceAtExecutionTime { get; private set; }
        public TypeValModel ReturnTypeVal { get; set; }
        public Type TargetType { get; private set; }
        public List<TypeValModel> Args { get; set; }
        public List<RecordedSubMethod> SubMethods { get; set; }
        public Exception MethodException { get; private set; }
        public string MethodName { get; private set; }

        public RecordedMethod(Type targetType, string serializedTarget, Object[] args, MethodBase method)
        {
            this.Identifier = Guid.NewGuid();
            this.TargetType = targetType;
            this.InstanceAtExecutionTime = serializedTarget;

            this.Args = new List<TypeValModel>();

            
            foreach(var arg in args ?? (new Object[0]))
            {
                this.Args.Add(new TypeValModel(arg.GetType(), arg));
            }

            this.ReturnTypeVal = new TypeValModel() { Type = ((MethodInfo)method).ReturnType };
        }

        public void CloseOutMethodWithReturnVal(Object returnVal)
        {
            this.ReturnTypeVal.Value = returnVal;
            this.ExecutionComplete = true;
        }

        public void CloseOutMethodWithException(Exception ex)
        {
            this.ExecutionComplete = true;
            this.MethodException = ex;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is RecordedMethod)) return false;

            var method2 = (RecordedMethod)obj;
            return (method2.Identifier.Equals(this.Identifier));
        }

    }
}
