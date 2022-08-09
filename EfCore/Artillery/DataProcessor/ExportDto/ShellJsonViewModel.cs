using Artillery.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artillery.DataProcessor.ExportDto
{
    public class ShellJsonViewModel
    {
        public double ShellWeight { get; set; }
        public string Caliber { get; set; }
        public GunJsonViewModel[] Guns { get; set; }
    }

    public class GunJsonViewModel
    {
        public string GunType { get; set; }
        public int GunWeight { get; set; }
        public double BarrelLength { get; set; }
        public string Range { get; set; }
    }
}
/*[
{
    "ShellWeight": 124.0,
    "Caliber": "155 mm HE ERFB RA-BB",
    "Guns": [
      {
        "GunType": "AntiAircraftGun",
        "GunWeight": 250138,
        "BarrelLength": 6.55,
        "Range": "Long-range"
      }
    ]
  },
    */
