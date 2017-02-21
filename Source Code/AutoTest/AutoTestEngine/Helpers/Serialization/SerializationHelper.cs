using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AutoTestEngine.Helpers.Serialization
{
    /// <summary>
    /// Wrapper around newtonsoft json to optimize for auto test application
    /// </summary>
    internal class SerializationHelper : ISerializationHelper
    {
        public SerializationResult Serialize(TypeValModel val)
        {
            string result;
            try
            {
                result = JsonConvert.SerializeObject(val.Value, Formatting.Indented);
            }
            catch(Exception ex)
            {
                return SerializationResult.InitFailedSerialization(val.Value, ex, val.Type);
            }

            return SerializationResult.InitSuccessfulSerialization(val.Value, result, val.Type);
        }

        public T Deserialize<T>(string serializedObject)
        {
            return JsonConvert.DeserializeObject<T>(serializedObject);
        }

        public object Desierialize(Type objectType, string serializedObject)
        {
           return JsonConvert.DeserializeObject(serializedObject);
        }
    }
}
