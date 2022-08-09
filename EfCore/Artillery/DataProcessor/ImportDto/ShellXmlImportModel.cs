using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Artillery.DataProcessor.ImportDto
{
    [XmlType("Shell")]
    public class ShellXmlImportModel
    {
        [XmlElement("ShellWeight")]
        [Range(2, 1_680)]
        public double ShellWeight { get; set; }
        [XmlElement("Caliber")]
        [StringLength(30, MinimumLength = 4)]
        public string Caliber { get; set; }
    }
}
/*ShellWeight – double in range  [2…1_680] (required)        !
⦁	Caliber – text with length [4…30] (required)               !
*/
/*<Shells>
  <Shell>
    <ShellWeight>50</ShellWeight>
    <Caliber>155 mm (6.1 in)</Caliber>
  </Shell>
*/
