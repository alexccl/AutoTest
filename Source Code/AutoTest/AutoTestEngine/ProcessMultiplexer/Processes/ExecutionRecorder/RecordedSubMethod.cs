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
        public TypeValModel ReturnTypeVal { get; set; }
        public Type TargetType { get; private set; }
        public List<TypeValModel> Args { get; set; }

        public RecordedSubMethod(Guid identifier)
        {
            this.Identifier = identifier;
        }
    }
}
