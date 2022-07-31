using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.DataTransferObjects.InputModel
{
    [XmlType("Part")]
    public class PartInputModel
    {
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("price")]
        public decimal Price { get; set; }
        [XmlElement("quantity")]
        public int Quantity { get; set; }
        [XmlElement("supplierId")]
        public int SupplierId { get; set; }
    }
}


/*
 <Parts>  //ROOT
    <Part>   // TYPE
        <name>Bonnet/hood</name> //ELEMENT
        <price>1001.34</price>
        <quantity>10</quantity>
        <supplierId>17</supplierId>
    </Part>
*/
