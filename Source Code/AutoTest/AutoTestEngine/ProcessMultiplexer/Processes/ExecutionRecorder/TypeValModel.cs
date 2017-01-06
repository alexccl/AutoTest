using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder
{
    class TypeValModel
    {
        public Type Type { get; set; }
        public Object Value { get; set; }

        public TypeValModel(Type type, Object value) : this()
        {
            this.Type = type;
            this.Value = value;
        }

        public TypeValModel()
        {

        }
    }
}
