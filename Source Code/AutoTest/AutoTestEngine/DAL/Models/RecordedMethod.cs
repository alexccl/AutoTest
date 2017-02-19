using AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.DAL.Models
{
    internal class RecordedMethod
    {
        public Guid Identifier { get; set; }
        public string InstanceAtExecutionTime { get; set; }
        public TypeValModel ReturnTypeVal { get; set; }
        public Type TargetType { get; private set; }
        public List<TypeValModel> Args { get; set; }
        public List<RecordedSubMethod> SubMethods { get; set; }
        public Exception MethodException { get; set; }
        public string MethodName { get; set; }

        public RecordedMethod()
        {

        }

        public RecordedMethod(RecordingMethod finishedMethod)
        {
            this.Identifier = finishedMethod.Identifier;
            this.InstanceAtExecutionTime = finishedMethod.InstanceAtExecutionTime;
            this.ReturnTypeVal = finishedMethod.ReturnTypeVal;
            this.TargetType = finishedMethod.TargetType;
            this.Args = finishedMethod.Args;
            this.SubMethods = finishedMethod.SubMethods;
            this.MethodException = finishedMethod.MethodException;
            this.MethodName = finishedMethod.MethodName;
        }
    }
}
