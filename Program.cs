using SenteApp.Interfaces;
using SenteApp.Models;
using SenteApp.Processing;
using SenteApp.Readers;
using SenteApp.Services;
using System;

namespace SenteApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var structurePath = "E:\\Code\\C#\\SenteApp\\TestFiles\\struktura.xml";
            var transfersPath = "E:\\Code\\C#\\SenteApp\\TestFiles\\przelewy.xml";
            IXmlReader<Structure> xmlStrucutreReader = new XmlReader<Structure>();
            IXmlReader<Transfers> xmlTransfersReader = new XmlReader<Transfers>();
            var structureResult = xmlStrucutreReader.ReadXml(structurePath);
            var transfersResult = xmlTransfersReader.ReadXml(transfersPath);
            Console.WriteLine(structureResult);
            Console.WriteLine(transfersResult);

            var provisionService = new ProvisionService();
            var displayService = new DisplayService();
            var result = provisionService.Calculate(structureResult, transfersResult);
            displayService.Display(result);
        }
    }
}
