
namespace Artillery.DataProcessor
{
    using Artillery.Data;
    using Artillery.Data.Models.Enums;
    using Artillery.DataProcessor.ExportDto;
    using Newtonsoft.Json;
    using SoftJail.DataProcessor;
    using System;
    using System.Linq;

    public class Serializer
    {
        public static string ExportShells(ArtilleryContext context, double shellWeight)
        {
            /*The given method in the project’s skeleton receives a double representing the shell weight.
             * Export all shells which weights more than the given and the guns which use this shell.
             * For each Shell, export its ShellWeight, Caliber, and Guns. Export only the guns which are AntiAircraftGun gun type.
             * For every gun export GunType, GunWeight, BarrelLength, and Range (if the range is bigger than 3000, export "Long-range",
             * otherwise export "Regular range"). Order the guns by GunWeight (descending). Order the shells by ShellWeight (ascending).*/
            var jsonView = context.Shells
                .Where(x => x.ShellWeight > shellWeight)
                .Select(x => new ShellJsonViewModel
                {
                    ShellWeight = x.ShellWeight,
                    Caliber = x.Caliber,
                    Guns = x.Guns.Where(x => x.GunType == GunType.AntiAircraftGun).Select(g => new GunJsonViewModel
                    {
                        GunType = g.GunType.ToString(),
                        GunWeight = g.GunWeight,
                        BarrelLength = g.BarrelLength,
                        Range = g.Range > 3000 ? "Long-range" : "Regular range"
                    })
                    .OrderByDescending(o => o.GunWeight)
                    .ToArray()
                })
                .OrderBy(o => o.ShellWeight)
                .ToArray();

            var result = JsonConvert.SerializeObject(jsonView, Formatting.Indented);
            return result;
        }

        public static string ExportGuns(ArtilleryContext context, string manufacturer)
        {
            /*Export all guns with a manufacturer equal to the given. For each gun
             * , export Manufacturer, GunType, BarrelLength, GunWeight, Range,
             * and Countries that use this gun. Select only the Countries which has ArmySize bigger than 4500000. 
             * For each country export CountryName and ArmySize. Order the countries by army size (ascending). Order guns by BarrelLength (ascending).*/

            var xmlView = context.Guns
                .Where(x => x.Manufacturer.ManufacturerName == manufacturer)
                .Select(x => new GunXmlViewModel
                {
                    Manufacturer = x.Manufacturer.ManufacturerName,
                    GunType = x.GunType.ToString(),
                    BarrelLength = x.BarrelLength,
                    GunWeight = x.GunWeight,
                    Range = x.Range,
                    Countries = x.CountriesGuns.Where(s => s.Country.ArmySize > 4500000).Select(a => new CountryXmlViewModel
                    {
                        Country = a.Country.CountryName,
                        ArmySize = a.Country.ArmySize
                    })
                    .OrderBy(o => o.ArmySize)
                    .ToArray()
                })
                .OrderBy(o => o.BarrelLength)
                .ToArray();
            var result = XmlConverter.Serialize<GunXmlViewModel>(xmlView, "Guns");
            return result;
        }
    }
}
