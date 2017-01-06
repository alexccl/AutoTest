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
        public bool ExeuctionComplete { get; private set; }
        public string InstanceAtExecutionTime { get; private set; }
        public TypeValModel ReturnTypeVal { get; set; }
        public Type TargetType { get; private set; }
        public List<TypeValModel> Args { get; set; }
        public List<RecordedSubMethod> SubMethods { get; set; }

        public RecordedMethod(Type targetType, string serializedTarget, Object[] args, MethodBase method)
        {
            this.Identifier = Guid.NewGuid();
            this.TargetType = targetType;
            this.InstanceAtExecutionTime = serializedTarget;

            this.Args = new List<TypeValModel>();
            foreach(var arg in args)
            {
                this.Args.Add(new TypeValModel(arg.GetType(), arg));
            }

            this.ReturnTypeVal = new TypeValModel() { Type = ((MethodInfo)method).ReturnType };
        }



        public void CloseOutMethod(Object returnVal)
        {
            this.ReturnTypeVal.Value = returnVal;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is RecordedMethod)) return false;

            var method2 = (RecordedMethod)obj;
            return (method2.Identifier.Equals(this.Identifier));
        }

    }
}
