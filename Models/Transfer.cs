using System;
using System.Xml.Serialization;

namespace SenteApp.Models
{
    [Serializable()]
    public class Transfer
    {

        [XmlAttribute(AttributeName = "od")]
        public int From { get; set; }

        [XmlAttribute(AttributeName = "kwota")]
        public int Amount { get; set; }

    }

    [Serializable()]
    [XmlRoot("przelewy")]
    public class Transfers
    {
        [XmlElement("przelew")]
        public Transfer[] AllTransfers { get; set; }
    }
}
