using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Theatre.DataProcessor.ImportDto
{
    [XmlType("Cast")]
    public class CastXmlImportModel
    {
        [XmlElement("FullName")]
        [Required]
        [StringLength(30, MinimumLength = 4)]
        public string FullName { get; set; }

        [XmlElement("IsMainCharacter")]
        public bool IsMainCharacter { get; set; }

        [XmlElement("PhoneNumber")]
        [Required]
        [RegularExpression(@"(\+4{2})-([0-9]{2})-([0-9]{3})-([0-9]{4})")]
        public string PhoneNumber { get; set; }

        [XmlElement("PlayId")]
        public int PlayId { get; set; }
    }
}



/*<Casts>
  <Cast>
    <FullName>Van Tyson</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-35-745-2774</PhoneNumber>
    <PlayId>26</PlayId>
  </Cast>
*/