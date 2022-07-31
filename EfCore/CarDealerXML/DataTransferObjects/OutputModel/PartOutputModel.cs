using System.Xml.Serialization;

namespace CarDealer.DataTransferObjects.OutputModel
{
    [XmlType("part")]
    public class PartOutputModel
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("price")]
        public decimal Price { get; set; }
    }
}