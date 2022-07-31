using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using CarDealer.Data;
using CarDealer.DTO;
using CarDealer.Models;
using Newtonsoft.Json;

namespace CarDealer
{
    public class StartUp
    {
        static IMapper mapper;
        public static void Main(string[] args)
        {
            var CarDealerContext = new CarDealerContext();
            //CarDealerContext.Database.EnsureDeleted();
            //CarDealerContext.Database.EnsureCreated();

            string suppliersJson = File.ReadAllText("../../../Datasets/suppliers.json"); // Намираме джейсон файла и го прочитаме
            ImportSuppliers(CarDealerContext, suppliersJson);

            string partsJson = File.ReadAllText("../../../Datasets/parts.json");
            ImportParts(CarDealerContext, partsJson);

            string carsJson = File.ReadAllText("../../../Datasets/cars.json");
            ImportCars(CarDealerContext, carsJson);

            string customersJson = File.ReadAllText("../../../Datasets/customers.json");
            ImportCustomers(CarDealerContext, customersJson);

            string salesJson = File.ReadAllText("../../../Datasets/sales.json");
            ImportSales(CarDealerContext, salesJson);


            Console.WriteLine(GetOrderedCustomers(CarDealerContext));
        }
        //Export
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                .Select(x => new
                {
                    car = new
                    {
                        Make = x.Car.Make,
                        Model = x.Car.Model,
                        TravelledDistance = x.Car.TravelledDistance
                    },
                    customerName = x.Customer.Name,
                    Discount = x.Discount.ToString("f2"),
                    price = x.Car.PartCars.Sum(z => z.Part.Price).ToString("f2"),
                    priceWithDiscount = (x.Car.PartCars.Sum(y => y.Part.Price) - x.Car.PartCars.Sum(p => p.Part.Price) * x.Discount / 100).ToString("f2")
                })
                .Take(10)
                .ToList();
            var result = JsonConvert.SerializeObject(sales, Formatting.Indented);
            return result;
        }
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(x => x.Sales.Count > 0)
                .Select(x => new
                {
                    fullName = x.Name,
                    boughtCars = x.Sales.Count,
                    spentMoney = x.Sales.Sum(y => y.Car.PartCars.Sum(z => z.Part.Price))
                })
                .OrderByDescending(x => x.spentMoney)
                .ThenByDescending(x => x.boughtCars)
                .ToList();
            var result = JsonConvert.SerializeObject(customers, Formatting.Indented);
            return result;
        }
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(x => new
                {
                    car = new
                    {
                        x.Make,
                        x.Model,
                        x.TravelledDistance
                    },
                    parts = x.PartCars.Select(y => new
                    {
                        y.Part.Name,
                        Price = y.Part.Price.ToString("f2")
                    })
                })
                .ToList();
            var result = JsonConvert.SerializeObject(cars, Formatting.Indented);
            return result;
        }
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(x => x.IsImporter == false)
                .Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    PartsCount = x.Parts.Count()
                })
                .ToList();
            var result = JsonConvert.SerializeObject(suppliers, Formatting.Indented);
            return result;
        }
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var toyotas = context.Cars
                .Where(x => EF.Functions.Like(x.Make, "Toyota"))
                .Select(x => new
                {
                    Id = x.Id,
                    Make = x.Make,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance
                })
                .OrderBy(x => x.Model)
                .ThenByDescending(x => x.TravelledDistance)
                .ToList();
            var result = JsonConvert.SerializeObject(toyotas, Formatting.Indented);
            return result;
        }
        public static string GetOrderedCustomers(CarDealerContext context)
        {
            /*Get all customers ordered by their birth date ascending.
             * If two customers are born on the same date first print those who are not young drivers
             * (e.g., print experienced drivers first). Export the list of customers to JSON in the format provided below.*/

            var users = context.Customers
                .OrderBy(x => x.BirthDate)
                .ThenBy(x => x.IsYoungDriver == true)
                .Select(x => new
                {
                    Name = x.Name,
                    BirthDate = x.BirthDate.ToString("dd/MM/yyyy"),
                    IsYoungDriver = x.IsYoungDriver
                })
                .ToList();
            var result = JsonConvert.SerializeObject(users, Formatting.Indented);
            return result;
        }
        //Import
        private static void InitialaizAutoMapper() // инициализираме АутоМапър
        {
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            });
            mapper = configMapper.CreateMapper();
        }
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var dtoSalesJson = JsonConvert.DeserializeObject<IEnumerable<SaleInputModel>>(inputJson);
            var sales = dtoSalesJson.Select(x => new Sale
            {
                CarId = x.CarId,
                CustomerId = x.CustomerId,
                Discount = x.Discount
            })
                .ToList();
            context.AddRange(sales);
            context.SaveChanges();
            return $"Successfully imported {sales.Count}.";
        }
        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var dtoCustomersJson = JsonConvert.DeserializeObject<IEnumerable<CustomerInputModel>>(inputJson);

            var customers = dtoCustomersJson.Select(x => new Customer
            {
                Name = x.Name,
                BirthDate = x.BirthDate,
                IsYoungDriver = x.IsYoungDriver
            })
            .ToList();
            context.AddRange(customers);
            context.SaveChanges();
            return $"Successfully imported {customers.Count}.";
        }
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var dtoCarsJson = JsonConvert.DeserializeObject<IEnumerable<CarInputModel>>(inputJson);
            List<Car> cars = new List<Car>();
            foreach (var dtoCar in dtoCarsJson)
            {
                Car newCar = new Car
                {
                    Make = dtoCar.Make,
                    Model = dtoCar.Model,
                    TravelledDistance = dtoCar.TravelledDistance
                };
                foreach (var partId in dtoCar.PartsId.Distinct())
                {
                    newCar.PartCars.Add(new PartCar
                    {
                        PartId = partId
                    });
                }
                cars.Add(newCar);
            }
            context.AddRange(cars);
            context.SaveChanges();
            return $"Successfully imported {cars.Count()}.";
        }
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            InitialaizAutoMapper();
            var supplierIds = context.Suppliers.Select(x => x.Id).ToList();
            var dtoPartsJson = JsonConvert.DeserializeObject<IEnumerable<PartInputModel>>(inputJson)
                .Where(x => supplierIds.Contains(x.SupplierId))
                .ToList();
            var parts = mapper.Map<IEnumerable<Part>>(dtoPartsJson);
            context.AddRange(parts);
            context.SaveChanges();
            return $"Successfully imported {parts.Count()}.";
        }
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            InitialaizAutoMapper();
            var dtoSuppliers = JsonConvert.DeserializeObject<IEnumerable<SupplierInputModel>>(inputJson); // Създаваме мап от Джейсън формата към нашия DTO
            var suppliers = mapper.Map<IEnumerable<Supplier>>(dtoSuppliers); // Създаваме мап от DTO към Модела в Базите Данни

            context.AddRange(suppliers);
            context.SaveChanges();
            return $"Successfully imported {suppliers.Count()}.";
        }
    }
}