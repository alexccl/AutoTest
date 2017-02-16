using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.DAL.Helpers
{
    internal interface IUnserializableTypeHelper
    {
        bool IsUnserializable(Type t);

        void AddUnserializableType(Type t);
    }
}
