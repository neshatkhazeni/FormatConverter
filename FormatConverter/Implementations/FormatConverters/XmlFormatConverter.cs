using System.Xml.Linq;
using FormatConverter.Interface;
using System.Xml.Serialization;

namespace FormatConverter.Implementations.FormatConverters
{
    public class XmlFormatConverter<T> : IFormatConverter<T>
    {
        public string FromObject(T input)
        {
            XDocument xdoc = new XDocument();
            using (var writer = xdoc.CreateWriter())
            {
                var serializer = new XmlSerializer(typeof(T), "");
                serializer.Serialize(writer, input);
            }
            return xdoc.ToString();
        }

        public T ToObject(string input)
        {
            var xdoc = XDocument.Parse(input);
            XmlSerializer serializer = new XmlSerializer(typeof(T), "");
            using (var reader = xdoc.CreateReader())
            {
                return (T)serializer.Deserialize(reader);
            }
        }

    }
}
