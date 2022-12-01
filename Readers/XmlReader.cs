using SenteApp.Interfaces;
using System.IO;
using System.Xml.Serialization;

namespace SenteApp.Readers
{
    internal class XmlReader<T> : IXmlReader<T>
    {
        private readonly XmlSerializer _xmlSerializer;

        public XmlReader()
        {
            _xmlSerializer = new XmlSerializer(typeof(T));
        }

        public T ReadXml(string path)
        {
            var reader = new StreamReader(path);
            var result = (T)_xmlSerializer.Deserialize(reader);
            reader.Close();
            return result;
        }
    }
}
