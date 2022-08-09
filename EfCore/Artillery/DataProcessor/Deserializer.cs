namespace Artillery.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using Artillery.Data;
    using Artillery.Data.Models;
    using Artillery.Data.Models.Enums;
    using Artillery.DataProcessor.ImportDto;
    using Newtonsoft.Json;
    using SoftJail.DataProcessor;

    public class Deserializer
    {
        private const string ErrorMessage =
                "Invalid data.";
        private const string SuccessfulImportCountry =
            "Successfully import {0} with {1} army personnel.";
        private const string SuccessfulImportManufacturer =
            "Successfully import manufacturer {0} founded in {1}.";
        private const string SuccessfulImportShell =
            "Successfully import shell caliber #{0} weight {1} kg.";
        private const string SuccessfulImportGun =
            "Successfully import gun {0} with a total weight of {1} kg. and barrel length of {2} m.";

        public static string ImportCountries(ArtilleryContext context, string xmlString)
        {
            //If any validation errors occur such as unvalid country name or army size,
            //do not import any part of the entity and append an error message "Invalid data." to the method output.
            StringBuilder output = new StringBuilder();
            var xmlCountryInsert = XmlConverter.Deserializer<CountryXmlImportModel>(xmlString, "Countries");
            var validCountries = new List<Country>();
            foreach (var currCountry in xmlCountryInsert)
            {
                if (!IsValid(currCountry))
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }

                var validCountry = new Country
                {
                    CountryName = currCountry.CountryName,
                    ArmySize = currCountry.ArmySize
                };

                validCountries.Add(validCountry);

                output.AppendLine(String.Format(SuccessfulImportCountry, validCountry.CountryName, validCountry.ArmySize));
            }
            context.Countries.AddRange(validCountries);
            context.SaveChanges();
            return output.ToString().TrimEnd();
        }

        public static string ImportManufacturers(ArtilleryContext context, string xmlString)
        {
            StringBuilder output = new StringBuilder();

            var xmlManufacturerInsert = XmlConverter.Deserializer<ManufacturXmlImportModel>(xmlString, "Manufacturers");

            var validManufacturers = new List<Manufacturer>();

            foreach (var currManufacturer in xmlManufacturerInsert)
            {

                if (!IsValid(currManufacturer))
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }
                var manufacturerNameExists = validManufacturers.FirstOrDefault(x => x.ManufacturerName == currManufacturer.ManufacturerName) != null;

                if (manufacturerNameExists)
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }

                var manufacturerFounded = currManufacturer.Founded.Split(", ");

                if (manufacturerFounded.Length < 3)
                {
                    continue;
                }

                var validManufacturer = new Manufacturer
                {
                    ManufacturerName = currManufacturer.ManufacturerName,
                    Founded = currManufacturer.Founded
                };

                validManufacturers.Add(validManufacturer);
                string townNameAndCountry = $"{manufacturerFounded[manufacturerFounded.Length - 2]}, {manufacturerFounded[manufacturerFounded.Length - 1]}";
                output.AppendLine(String.Format(SuccessfulImportManufacturer, validManufacturer.ManufacturerName, townNameAndCountry));

            }
            context.AddRange(validManufacturers);
            context.SaveChanges();

            return output.ToString().TrimEnd();
        }

        public static string ImportShells(ArtilleryContext context, string xmlString)
        {
            StringBuilder output = new StringBuilder();

            var xmlShelInsert = XmlConverter.Deserializer<ShellXmlImportModel>(xmlString, "Shells");

            var validShells = new List<Shell>();

            foreach (var currShell in xmlShelInsert)
            {
                if (!IsValid(currShell))
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }

                var validShell = new Shell
                {
                    ShellWeight = currShell.ShellWeight,
                    Caliber = currShell.Caliber
                };
                validShells.Add(validShell);

                output.AppendLine(String.Format(SuccessfulImportShell, validShell.Caliber, validShell.ShellWeight));

            }
            context.Shells.AddRange(validShells);
            context.SaveChanges();
            return output.ToString().TrimEnd();
        }

        public static string ImportGuns(ArtilleryContext context, string jsonString)
        {
            var output = new StringBuilder();

            var jsonGunInsert = JsonConvert.DeserializeObject<GunJsonImportModel[]>(jsonString);

            var validGuns = new List<Gun>();

            foreach (var currGun in jsonGunInsert)
            {
                if (!IsValid(currGun))
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }

                var validGun = new Gun
                {
                    ManufacturerId = currGun.ManufacturerId,
                    GunWeight = currGun.GunWeight,
                    BarrelLength = currGun.BarrelLength,
                    NumberBuild = currGun.NumberBuild,
                    Range = currGun.Range,
                    GunType = Enum.Parse<GunType>(currGun.GunType),
                    ShellId = currGun.ShellId,
                    CountriesGuns = currGun.Countries.Select(x => new CountryGun
                    {
                        CountryId = x.Id
                    })
                    .ToArray()
                };

                validGuns.Add(validGun);

                output.AppendLine(String.Format(SuccessfulImportGun, validGun.GunType, validGun.GunWeight, validGun.BarrelLength));
            }
            context.Guns.AddRange(validGuns);
            context.SaveChanges();
            return output.ToString().TrimEnd();
        }
        private static bool IsValid(object obj)
        {
            var validator = new ValidationContext(obj);
            var validationRes = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validator, validationRes, true);
            return result;
        }
    }
}
