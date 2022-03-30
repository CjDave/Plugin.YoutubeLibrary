
using Newtonsoft.Json;

namespace Plugin.Youtube.Utils
{
    internal static class serializer
    {
        public static T jsonConvert<T>(string json)
        {
            T t = JsonConvert.DeserializeObject<T>(json);
            return t;
        }
    }

}
