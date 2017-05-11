using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Runtime.Serialization;
using AutoTestEngine.Attributes;

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
                    TypeNameHandling = TypeNameHandling.All,
                    NullValueHandling = NullValueHandling.Ignore
                };
            }
        }
    }

    public class MyContractResolver : Newtonsoft.Json.Serialization.DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                            .Where(x => !x.CustomAttributes.Any(z => z.AttributeType == typeof(Dependency)))
                            .Select(p => CreateProperty(p, memberSerialization))
                        .Union(type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                            .Where(x => !x.CustomAttributes.Any(z => z.AttributeType == typeof(Dependency)))
                            .Select(f => CreateProperty(f, memberSerialization)))
                        .ToList();
            props.ForEach(p => { p.Writable = true; p.Readable = true; });
            return props;
        }

        protected override JsonProperty CreateProperty(MemberInfo member,
                                 MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);
            //property.ShouldSerialize = instance =>
            //{
            //    if (instance != null && instance.GetType().FullName.Contains("DynamicModule.ns.Wrapped"))
            //    {
            //        property.Ignored = true;
            //        property.ShouldSerialize = i => false;
            //        return false;
            //    }
            //
            //    return true;
            //};
            

            
            //t
            return property;
        }
    }
}
