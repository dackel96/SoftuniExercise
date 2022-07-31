using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.DataTransferObjects.OutputModel
{
    [XmlType("car")]
    public class CarWithPartsOutputModel
    {
        [XmlAttribute("make")]
        public string Make { get; set; }
        [XmlAttribute("model")]
        public string Model { get; set; }
        [XmlAttribute("travelled-distance")]
        public long TravelledDistance { get; set; }
        [XmlArray("parts")]
        public PartOutputModel[] Parts { get; set; }
    }
}
