using Artillery.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Artillery.DataProcessor.ImportDto
{
    public class GunJsonImportModel
    {
        public int ManufacturerId { get; set; }
        [Range(100,1_350_000)]
        public int GunWeight { get; set; }
        [Range(2.00,35.00)]
        public double BarrelLength { get; set; }
        public int? NumberBuild { get; set; }
        [Range(1,100_000)]
        public int Range { get; set; }
        [Required]
        [EnumDataType(typeof(GunType))]
        public string GunType { get; set; }
        public int ShellId { get; set; }
        public CountryJsonImportModel[] Countries { get; set; }
    }

    public class CountryJsonImportModel
    {
        public int Id { get; set; }
    }
}

/*GunWeight– integer in range [100…1_350_000] (required)
⦁	BarrelLength – double in range [2.00….35.00] (required)

⦁	NumberBuild – integer
⦁	Range – integer in range [1….100_000] (required)*/
/* {
    "ManufacturerId": 14,
    "GunWeight": 531616,
    "BarrelLength": 6.86,
    "NumberBuild": 287,
    "Range": 120000,
    "GunType": "Howitzer",
    "ShellId": 41,
    "Countries": [
      { "Id": 86 },
      { "Id": 57 },
      { "Id": 64 },
      { "Id": 74 },
      { "Id": 58 }
    ]*/
