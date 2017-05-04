using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.Helpers.Serialization
{
    public class SerializedValue
    {
        public Type Type { get; set; }
        public string Value { get; set; }

        public SerializedValue() { }
        public SerializedValue(Type type, string value)
        {
            this.Type = type;
            this.Value = value;
        }
    }
}
