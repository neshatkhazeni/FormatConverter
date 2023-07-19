using FormatConverter.Interface;
using Newtonsoft.Json;

namespace FormatConverter.Implementations.FormatConverters
{
    public class JsonFormatConverter<T> : IFormatConverter<T>
    {
        public string FromObject(T input)
        {
            return JsonConvert.SerializeObject(input);
        }

        public T ToObject(string input)
        {
            return JsonConvert.DeserializeObject<T>(input);
        }
    }
}
