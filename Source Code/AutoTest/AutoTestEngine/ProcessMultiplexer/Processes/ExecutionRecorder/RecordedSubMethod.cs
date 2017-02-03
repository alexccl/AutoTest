using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder
{
    internal class RecordedSubMethod
    {
        public Guid Identifier { get; private set; }
        public bool ExeuctionComplete { get; private set; }
        public TypeValModel ReturnTypeVal { get; private set; }
        public Type TargetType { get; private set; }
        public List<TypeValModel> Args { get; private set; }
        public string MethodName { get; private set; }
        public Exception MethodException { get; private set; }


        //TODO add a contructor to populate properties and methods to help closing it out
        public RecordedSubMethod(Guid identifier, Type targetType, List<TypeValModel> args, string methodName)
        {
            this.Identifier = identifier;
            this.TargetType = targetType;
            this.Args = args;
            this.MethodName = methodName;
        }

        public void CloseOutMethodWithReturnVal(object returnVal)
        {

        }

        public void CloseOutMethodWithException(Exception ex)
        {

        }
    }
}
