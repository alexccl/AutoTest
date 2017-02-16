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
    internal static class SerializationHelper
    {
        public static SerializationResult Serialize(object obj)
        {
            string result;
            try
            {
                result = JsonConvert.SerializeObject(obj, Formatting.Indented);
            }
            catch(Exception ex)
            {
                return SerializationResult.InitFailedSerialization(obj, ex);
            }

            return SerializationResult.InitSuccessfulSerialization(obj, result);
        }

        public static T Deserialize<T>(string serializedObject)
        {
            return JsonConvert.DeserializeObject<T>(serializedObject);
        }
    }
}
