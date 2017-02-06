using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder
{
    class TypeValModel
    {
        private Type _type;
        public Type Type {
            get
            {
                return _type;
            }
            set
            {
                if (!IsValidSet(value, _value)) throw new AutoTestEngineException("Tried to assign the wrong type for the value on an instance of TypeValModel");

                _type = value;
            }
        }

        private Object _value;
        public Object Value {
            get
            {
                return _value;
            }
            set
            {
                if (!IsValidSet(_type, value)) throw new AutoTestEngineException("Tried to assign the wrong value type on an instance of TypeValModel");
                _value = value;
            }
        }

        public TypeValModel(Type type, Object value) : this()
        {
            this.Type = type;
            this.Value = value;
        }

        public TypeValModel()
        {

        }

        private bool IsValidSet(Type type, Object val)
        {
            if (type == null || val == null) return true;

            return (val.GetType().Equals(type));
        }
    }
}
