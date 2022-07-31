using CarDealer.Data;
using CarDealer.DataTransferObjects.InputModel;
using CarDealer.DataTransferObjects.OutputModel;
using CarDealer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            string supplierXml = File.ReadAllText("./Datasets/suppliers.xml");
            ImportSuppliers(context, supplierXml);

            string partsXml = File.ReadAllText("./Datasets/parts.xml");
            ImportParts(context, partsXml);

            string carsXml = File.ReadAllText("./Datasets/cars.xml");
            ImportCars(context, carsXml);

            string customersXml = File.ReadAllText("./Datasets/customers.xml");
            ImportCustomers(context, customersXml);

            string salesXml = File.ReadAllText("./Datasets/sales.xml");
            ImportSales(context, salesXml);

            var result = GetSalesWithAppliedDiscount(context);
            Console.WriteLine(result);
        }
        //EXPORT
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            const string root = "sales";
            var sales = context.Sales
                .Select(x => new SalesOutputModel
                {
                    Car = new CarOutput
                    {
                        Make = x.Car.Make,
                        Model = x.Car.Model,
                        TravelledDistance = x.Car.TravelledDistance
                    },
                    Discount = x.Discount,
                    CustomerName = x.Customer.Name,
                    Price = x.Car.PartCars.Sum(x => x.Part.Price),
                    PriceWithDiscount = x.Car.PartCars.Sum(p => p.Part.Price) - x.Car.PartCars.Sum(p => p.Part.Price) * (x.Discount / 100)
                })
                .ToArray();
            XmlSerializer serializer = new XmlSerializer(typeof(SalesOutputModel[]), new XmlRootAttribute(root));
            StringWriter writer = new StringWriter();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            serializer.Serialize(writer, sales, ns);
            var result = writer.ToString();
            return result;
        }
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            const string root = "customers";
            var customers = context.Customers
                .Where(x => x.Sales.Count > 0)
                .Select(x => new CustomersOutputModel
                {
                    FullName = x.Name,
                    BoughtCars = x.Sales.Count,
                    SpentMoney = x.Sales.Select(c => c.Car).SelectMany(p => p.PartCars).Sum(m => m.Part.Price)
                })
                .OrderByDescending(o => o.SpentMoney)
                .ToArray();
            XmlSerializer serializer = new XmlSerializer(typeof(CustomersOutputModel[]), new XmlRootAttribute(root));
            StringWriter writer = new StringWriter();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            serializer.Serialize(writer, customers, ns);
            var result = writer.ToString();
            return result;
        }
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            const string root = "cars";
            var cars = context.Cars
                .Select(x => new CarWithPartsOutputModel
                {
                    Make = x.Make,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance,
                    Parts = x.PartCars.Select(p => new PartOutputModel
                    {
                        Name = p.Part.Name,
                        Price = p.Part.Price
                    })
                    .OrderByDescending(o => o.Price)
                    .ToArray()
                })
                .OrderByDescending(o => o.TravelledDistance)
                .ThenBy(o => o.Model)
                .Take(5)
                .ToArray();
            XmlSerializer serializer = new XmlSerializer(typeof(CarWithPartsOutputModel[]), new XmlRootAttribute(root));
            StringWriter writer = new StringWriter();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            serializer.Serialize(writer, cars, ns);
            var result = writer.ToString();
            return result;
        }
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            const string root = "suppliers";
            var suppliers = context.Suppliers
                .Where(x => x.IsImporter == false)
                .Select(x => new SupplierOutputModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    PartsCount = x.Parts.Count()
                })
                .ToArray();
            XmlSerializer serializer = new XmlSerializer(typeof(SupplierOutputModel[]), new XmlRootAttribute(root));
            StringWriter writer = new StringWriter();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            serializer.Serialize(writer, suppliers, ns);
            var result = writer.ToString();
            return result;
        }
        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            const string root = "cars";
            var bmws = context.Cars
                .Where(x => x.Make == "BMW")
                .Select(x => new BmwOutputModel
                {
                    Id = x.Id,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance
                })
                .OrderBy(x => x.Model)
                .ThenByDescending(x => x.TravelledDistance)
                .ToArray();
            XmlSerializer serializer = new XmlSerializer(typeof(BmwOutputModel[]), new XmlRootAttribute(root));
            StringWriter writer = new StringWriter();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            serializer.Serialize(writer, bmws, ns);
            var result = writer.ToString();
            return result;
        }
        public static string GetCarsWithDistance(CarDealerContext context)
        {
            const string root = "cars";
            var cars = context.Cars
                .Where(x => x.TravelledDistance > 2_000_000)
                .Select(x => new CarOutputModel
                {
                    Make = x.Make,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance
                })
                .OrderBy(x => x.Make)
                .ThenBy(x => x.Model)
                .Take(10)
                .ToArray();
            XmlSerializer serializer = new XmlSerializer(typeof(CarOutputModel[]), new XmlRootAttribute(root));
            StringWriter writer = new StringWriter();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            serializer.Serialize(writer, cars, ns);
            var result = writer.ToString();
            return result;

        }
        //IMPORT
        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            const string root = "Sales";

            XmlSerializer serializer = new XmlSerializer(typeof(SaleInputModel[]), new XmlRootAttribute(root));

            StringReader reader = new StringReader(inputXml);

            var rawResult = serializer.Deserialize(reader) as SaleInputModel[];

            var carIds = context.Cars
                .Select(x => x.Id)
                .ToList();

            var sales = rawResult
                .Where(x => carIds.Contains(x.CarId))
                .Select(x => new Sale
                {
                    CarId = x.CarId,
                    CustomerId = x.CustomerId,
                    Discount = x.Discount
                })
                .ToList();

            context.AddRange(sales);

            context.SaveChanges();

            return $"Successfully imported {sales.Count}";
        }
        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            const string root = "Customers";

            XmlSerializer serializer = new XmlSerializer(typeof(CustomerInputModel[]), new XmlRootAttribute(root));

            StringReader reader = new StringReader(inputXml);

            var rawResult = serializer.Deserialize(reader) as CustomerInputModel[];

            var customers = rawResult
                .Select(x => new Customer
                {
                    Name = x.Name,
                    BirthDate = x.BirthDate,
                    IsYoungDriver = x.IsYoungDriver
                })
                .ToList();

            context.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count}";
        }
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            const string root = "Cars";

            XmlSerializer serializer = new XmlSerializer(typeof(CarInputModel[]), new XmlRootAttribute(root));

            StringReader reader = new StringReader(inputXml);

            var rawResult = serializer.Deserialize(reader) as CarInputModel[];

            var partsIds = context.Parts
                .Select(x => x.Id)
                .ToList();
            var cars = rawResult
                .Select(x => new Car
                {
                    Make = x.Make,
                    Model = x.Model,
                    TravelledDistance = x.TraveledDistance,
                    PartCars = x.Parts.Select(x => x.Id)
                        .Distinct()
                        .Intersect(partsIds)
                        .Select(y => new PartCar
                        {
                            PartId = y
                        })
                        .ToList()
                })
                .ToList();
            context.AddRange(cars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count()}";
        }
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            const string root = "Parts";

            XmlSerializer serializer = new XmlSerializer(typeof(PartInputModel[]), new XmlRootAttribute(root));

            StringReader reader = new StringReader(inputXml);

            var rawResult = serializer.Deserialize(reader) as PartInputModel[];

            var suppliersId = context.Suppliers
                .Select(x => x.Id)
                .ToList();

            List<Part> parts = rawResult
                .Where(x => suppliersId.Contains(x.SupplierId))
                .Select(x => new Part
                {
                    Name = x.Name,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    SupplierId = x.SupplierId
                })
            .ToList();

            context.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count}";
        }
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            const string root = "Suppliers";

            XmlSerializer serializer = new XmlSerializer(typeof(SupplierInputModel[]), new XmlRootAttribute(root));

            StringReader reader = new StringReader(inputXml);

            var result = serializer.Deserialize(reader) as SupplierInputModel[];

            List<Supplier> suppliers = result.Select(x => new Supplier
            {
                Name = x.Name,
                IsImporter = x.IsImporter
            })
            .ToList();
            context.AddRange(suppliers);
            context.SaveChanges();
            return $"Successfully imported {suppliers.Count}";
        }
    }
}