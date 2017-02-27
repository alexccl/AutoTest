using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.TestGeneration
{
    internal class SerializedArg
    {
        public Type Type { get; private set; }
        public string GeneratedArgName { get; private set; }
        public string SerializedArgInstance { get; private set; }

        public SerializedArg(Type type, string generatedName, string instance)
        {
            this.Type = type;
            this.GeneratedArgName = generatedName;
            this.SerializedArgInstance = instance;
        }
    }
}
