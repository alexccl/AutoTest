using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.DAL.Models
{
    class UnserializableType
    {
        public Type InvalidType { get; set; }
        public UnserializableType(Type type)
        {
            this.InvalidType = type;
        }

        public override bool Equals(object obj)
        {
            if (!obj.GetType().Equals(this.GetType()))
            {
                return false;
            }

            var type2 = obj as UnserializableType;
            return type2.InvalidType.Equals(this.InvalidType);
        }
    }
}
