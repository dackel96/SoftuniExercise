using System.Xml.Serialization;

namespace CarDealer.DataTransferObjects.InputModel
{
    [XmlType("partId")]
    public class CarPartsInputModel
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}

// < partId <--TYPE|| Attribute -->id = "38" />