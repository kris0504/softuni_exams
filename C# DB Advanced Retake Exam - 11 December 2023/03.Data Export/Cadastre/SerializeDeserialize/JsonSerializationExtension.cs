using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Cadastre.SerializeDeserialize
{
    public static class JsonSerializationExtension
    {
        public static T DeserializeFromJson<T>(this string jsonString)
        {
            var jsonSerializer = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,

            };
            T result = JsonConvert.DeserializeObject<T>(jsonString, jsonSerializer);
            return result;
        }
        public static string SerializeToJson<T>(this T obj)
        {
            var serializer = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
                Converters = new List<JsonConverter>()
                {
                    new StringEnumConverter()
                }
            };
            string result = JsonConvert.SerializeObject(obj, serializer);
            return result;
        }
    }
}
