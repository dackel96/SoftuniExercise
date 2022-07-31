using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.DTO;
using ProductShop.Models;
using Microsoft.EntityFrameworkCore;
namespace ProductShop
{
    public class StartUp
    {
        static IMapper mapper;
        public static void Main(string[] args)
        {
            var productShopContext = new ProductShopContext();
            //////////\\\\\\IMPORT//////\\\\\\\\\\\\\
            //productShopContext.Database.EnsureDeleted();
            //productShopContext.Database.EnsureCreated();
            //
            //string usersJson = File.ReadAllText("../../../Datasets/users.json");
            //ImportUsers(productShopContext, usersJson);
            //
            //string productsJson = File.ReadAllText("../../../Datasets/products.json");
            //ImportProducts(productShopContext, productsJson);
            //
            //string categoryJson = File.ReadAllText("../../../Datasets/categories.json");
            //ImportCategories(productShopContext, categoryJson);
            //
            //string categoryProducts = File.ReadAllText("../../../Datasets/categories-products.json");
            //var result = ImportCategoryProducts(productShopContext, categoryProducts);
            //
            //Console.WriteLine(result);
            /////EXPORT\\\\\\\
            var result = GetUsersWithProducts(productShopContext);
            Console.WriteLine(result);
        }
        private static void InitialaizAutoMapper()
        {
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });
            mapper = configMapper.CreateMapper();
        }
        // EXPORT \\
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context.Users
                .Include(x => x.ProductsSold)
                .ToList()
                .Where(x => x.ProductsSold.Any(y => y.BuyerId != null))
                .Select(x => new
                {
                    firstName = x.FirstName,
                    lastName = x.LastName,
                    age = x.Age,
                    soldProducts = new
                    {
                        count = x.ProductsSold.Where(y => y.BuyerId != null).Count(),
                        products = x.ProductsSold.Where(y => y.BuyerId != null).Select(z => new
                        {
                            name = z.Name,
                            price = z.Price
                        })
                    }
                })
                .OrderByDescending(x => x.soldProducts.count)
                .ToList();
            var jsonserializeSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            var resultObject = new
            {
                usersCount = users.Count,
                users = users
            };
            var result = JsonConvert.SerializeObject(resultObject, Formatting.Indented, jsonserializeSettings);
            return result;

        }
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .Select(x => new
                {
                    category = x.Name,
                    productsCount = x.CategoryProducts.Count(),
                    averagePrice = x.CategoryProducts.Average(y => y.Product.Price).ToString("f2"),
                    totalRevenue = x.CategoryProducts.Sum(y => y.Product.Price).ToString("f2")
                })
                .OrderByDescending(x => x.productsCount)
                .ToList();
            var result = JsonConvert.SerializeObject(categories, Formatting.Indented);
            return result;
        }
        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(x => x.ProductsSold.Any(y => y.BuyerId != null))
                .Select(x => new
                {
                    firstName = x.FirstName,
                    lastName = x.LastName,
                    soldProducts = x.ProductsSold.Where(p => p.BuyerId != null).Select(y => new
                    {
                        name = y.Name,
                        price = y.Price,
                        buyerFirstName = y.Buyer.FirstName,
                        buyerLastName = y.Buyer.LastName
                    })
                    .ToList()
                })
                .OrderBy(x => x.lastName)
                .ThenBy(x => x.firstName)
                .ToList();
            var result = JsonConvert.SerializeObject(users, Formatting.Indented);
            return result;
        }
        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(x => x.Price >= 500 && x.Price <= 1000)
                .Select(x => new
                {
                    name = x.Name,
                    price = x.Price,
                    seller = x.Seller.FirstName + " " + x.Seller.LastName
                })
                .OrderBy(x => x.price)
                .ToList();
            var result = JsonConvert.SerializeObject(products, Formatting.Indented);
            return result;
        }


        // IMPORT
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            InitialaizAutoMapper();
            var dtoUsers = JsonConvert.DeserializeObject<IEnumerable<UserInputModel>>(inputJson);

            var users = mapper.Map<IEnumerable<User>>(dtoUsers);

            context.AddRange(users);
            context.SaveChanges();
            return $"Successfully imported {users.Count()}";
        }
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            InitialaizAutoMapper();
            var dtoProducts = JsonConvert.DeserializeObject<IEnumerable<ProductInputModel>>(inputJson);
            var products = mapper.Map<IEnumerable<Product>>(dtoProducts);
            context.AddRange(products);
            context.SaveChanges();
            return $"Successfully imported {products.Count()}";
        }
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            InitialaizAutoMapper();
            var dtoCategories = JsonConvert
                .DeserializeObject<IEnumerable<CategoryInputModel>>(inputJson)
                .Where(x => x.Name != null)
                .ToList();
            var categories = mapper.Map<IEnumerable<Category>>(dtoCategories);
            context.AddRange(categories);
            context.SaveChanges();
            return $"Successfully imported {categories.Count()}";
        }
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            InitialaizAutoMapper();
            var dtoCategoryProducts = JsonConvert.DeserializeObject<IEnumerable<CategoryProductsInputModel>>(inputJson);
            var categoryProducts = mapper.Map<IEnumerable<CategoryProduct>>(dtoCategoryProducts);

            context.AddRange(categoryProducts);
            context.SaveChanges();
            return $"Successfully imported {categoryProducts.Count()}";
        }
    }
}