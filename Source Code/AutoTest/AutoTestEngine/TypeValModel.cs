using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine
{
    public class TypeValModel
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

        public TypeValModel(Type type, Object value)
        {
            this.Type = type;
            this.Value = value;
        }
        public TypeValModel()
        {

        }

        private bool IsValidSet(Type type, Object val)
        {
            return true;
            if (type == null || val == null) return true;
            var valType = val.GetType();

            return (IsTypeEqual(type, valType) ||
                    AreEqualThroughInheritance(type, valType) ||
                    AreGenericsEqual(type, valType));

        }

        private bool IsTypeEqual(Type t1, Type t2)
        {
            return t1.Equals(t2);
        }

        private bool AreEqualThroughInheritance(Type baseType, Type derivedType)
        {
            if (IsTypeEqual(baseType, derivedType)) return true;
            return baseType.IsAssignableFrom(derivedType);
        }


        private bool AreGenericsEqual(Type baseType, Type derivedType)
        {
            if (!baseType.IsGenericType || !derivedType.IsGenericType) return false;

            var baseGeneric = baseType.GetGenericTypeDefinition();
            var derivedGeneric = derivedType.GetGenericTypeDefinition();

            if (!AreEqualThroughInheritance(baseGeneric, derivedGeneric)) return false;

            var baseGenericParam = baseType.GenericTypeArguments[0];
            var derivedGenericParam = baseType.GenericTypeArguments[0];

            return AreEqualThroughInheritance(baseGenericParam, derivedGenericParam);
        }
    }
}
