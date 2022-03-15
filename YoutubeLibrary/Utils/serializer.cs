
using Newtonsoft.Json;

namespace Plugin.Youtube.Utils
{
    internal class serializer
    {
        public T jsonConvert<T>(string json)
        {
            T t = JsonConvert.DeserializeObject<T>(json);
            return t;
        }
    }

}
