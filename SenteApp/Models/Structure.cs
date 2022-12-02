using System;
using System.Xml.Serialization;

namespace SenteApp.Models
{
    [Serializable()]
    public class Participant
    {
        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; }

        [XmlElement("uczestnik")]
        public Participant[] Subordinates { get; set; }

        public Participant Supervisor { get; set; }

        public int Depth { get; set; }

        public int NotLinkedSubordinates { get; set; }

        public int Money { get; set; }
    }

    [Serializable()]
    [XmlRoot("struktura")]
    public class Structure
    {
        [XmlElement("uczestnik")]
        public Participant Participant { get; set; }
    }
}
