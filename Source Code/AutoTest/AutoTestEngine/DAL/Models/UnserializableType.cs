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
    }
}
