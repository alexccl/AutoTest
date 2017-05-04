using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder
{
    internal class RecordedSubMethod
    {
        public Guid Identifier { get; private set; }
        public bool ExecutionComplete { get; private set; }
        public object ReturnVal { get; private set; }
        public Type TargetType { get; private set; }

        public List<object> Args { get; private set; }
        public MethodMetaData MethodData { get; private set; }

        public Exception MethodException { get; private set; }

        public RecordedSubMethod() { }
        //TODO add a contructor to populate properties and methods to help closing it out
        public RecordedSubMethod(Guid identifier, Type targetType, List<object> args, Type returnType, MethodBase method)
        {
            this.Identifier = identifier;
            this.TargetType = targetType;
            this.Args = args;
            this.MethodData = new MethodMetaData(method);


            this.ExecutionComplete = false;
        }

        public void CloseOutMethodWithReturnVal(object returnVal)
        {
            this.ExecutionComplete = true;
            this.ReturnVal = returnVal;
        }

        public void CloseOutMethodWithException(Exception ex)
        {
            this.ExecutionComplete = true;
            this.MethodException = ex;
        }
    }
}
