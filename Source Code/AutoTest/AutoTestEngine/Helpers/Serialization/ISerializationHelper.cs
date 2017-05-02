using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.Helpers.Serialization
{
    internal interface ISerializationHelper
    {
        SerializationResult Serialize(object value);

        T Deserialize<T>(string serializedObject);

        object Desierialize(Type objectType, string serializedObject);
    }
}
