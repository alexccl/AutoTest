using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.Helpers.Serialization
{
    class SerializationException : Exception
    {
        public Type OffendingType { get; private set; }
        public SerializationException(string message, Exception innerException, Type offendingType) : base(message, innerException)
        {
            this.OffendingType = offendingType;
        }
    }
}
