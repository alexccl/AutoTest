using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.Helpers.Serialization
{
    internal interface ISerializationHelper
    {
        string Serialize(object obj);

        T Deserialize<T>(string serializedObject);
    }
}
