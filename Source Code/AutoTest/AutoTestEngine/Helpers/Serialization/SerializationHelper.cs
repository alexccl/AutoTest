using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace AutoTestEngine.Helpers.Serialization
{
    /// <summary>
    /// Wrapper around newtonsoft json to optimize for auto test application
    /// </summary>
    internal class SerializationHelper : ISerializationHelper
    {
        public SerializationResult Serialize(object val)
        {
            string result;
            try
            {
                result = JsonConvert.SerializeObject(val, this.Settings);

               // result = JsonConvert.SerializeObject(val.Value);
            }
            catch(Exception ex)
            {
                return SerializationResult.InitFailedSerialization(val, ex, val?.GetType());
            }

            return SerializationResult.InitSuccessfulSerialization(val, result, val?.GetType());
        }

        public T Deserialize<T>(string serializedObject)
        {
            var x = typeof(T);
            return (T)JsonConvert.DeserializeObject(serializedObject, this.Settings);
        }

        public object Desierialize(Type objectType, string serializedObject)
        {
            return JsonConvert.DeserializeObject(serializedObject, this.Settings);
        }

        private JsonSerializerSettings Settings
        {
            get
            {
                return new JsonSerializerSettings()
                {
                    ContractResolver = new MyContractResolver(),
                    TypeNameHandling = TypeNameHandling.All
                };
            }
        }
    }

    public class MyContractResolver : Newtonsoft.Json.Serialization.DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                            .Select(p => base.CreateProperty(p, memberSerialization))
                        .Union(type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                   .Select(f => base.CreateProperty(f, memberSerialization)))
                        .ToList();
            props.ForEach(p => { p.Writable = true; p.Readable = true; });
            return props;
        }
    }
}
