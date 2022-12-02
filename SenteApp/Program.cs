using SenteApp.Interfaces;
using SenteApp.Models;
using SenteApp.Processing;
using SenteApp.Readers;
using System.IO;
using System.Reflection;

namespace SenteApp
{
    internal class Program
    {
        private static readonly string StructureFilePath = "Files\\struktura.xml";

        private static readonly string TransferFilePath = "Files\\przelewy.xml";

        static void Main(string[] args)
        {
            var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var structurePath = Path.Combine(basePath, StructureFilePath);
            var transfersPath = Path.Combine(basePath, TransferFilePath);

            IXmlReader<Structure> xmlStrucutreReader = new XmlReader<Structure>();
            IXmlReader<Transfers> xmlTransfersReader = new XmlReader<Transfers>();

            var structureResult = xmlStrucutreReader.ReadXml(structurePath);
            var transfersResult = xmlTransfersReader.ReadXml(transfersPath);

            IProvisionService provisionService = new ProvisionService();

            provisionService.Calculate(structureResult, transfersResult);
            provisionService.Display();
        }
    }
}
