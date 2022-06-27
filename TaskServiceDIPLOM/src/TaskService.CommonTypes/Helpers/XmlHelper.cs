using System.Xml.Serialization;

namespace TaskService.CommonTypes.Helpers
{
    public class XmlHelper
    {
        public string Serialize<T>(T obj)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, obj);
                return textWriter.ToString();
            }
        }

        public T Deserailize<T>(string obj)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StringReader textReader = new StringReader(obj))
            {
                return (T)xmlSerializer.Deserialize(textReader);
            }
        }
    }
}
