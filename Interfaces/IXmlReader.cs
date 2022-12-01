namespace SenteApp.Interfaces
{
    public interface IXmlReader<T>
    {
        T ReadXml(string path);
    }
}
