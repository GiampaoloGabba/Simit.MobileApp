using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace Simit.Core
{
    public static class JsonDataReader
    {

        public static T Read<T>(string jsonFileName)
        {
            T serialized;
            var assembly  = typeof(App).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Resources.{jsonFileName}");

            if (stream == null)
                return default;

            using (var reader = new StreamReader(stream))
            {
                var jsonString   = reader.ReadToEnd();
                serialized = JsonConvert.DeserializeObject<T>(jsonString);
            }

            return serialized;
        }
    }
}
