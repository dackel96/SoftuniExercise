using Microsoft.AspNetCore.Mvc;
using MVC_Introduction.Models;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using System.Text.Json;

namespace MVC_Introduction.Controllers
{
    public class ProductsController : Controller
    {
        private IEnumerable<ProductViewModel> products
            = new List<ProductViewModel>()
            {
                new ProductViewModel()
                {
                    Id = 1,
                    Name = "Tatto",
                    Price = 100.00m
                },
                new ProductViewModel()
                {
                    Id = 2,
                    Name = "Piercing",
                    Price = 150.00m
                },
                new ProductViewModel()
                {
                    Id=3,
                    Name = "T-Shirt",
                    Price = 50.00m
                }
            };
        public IActionResult Index()
        {
            return View();
        }
        [ActionName("My-Products")]
        public IActionResult All(string kewword)
        {
            if (kewword != null)
            {
                var productsByName = products.Where(x => x.Name.ToLower().Contains(kewword.ToLower()));
                return View(productsByName);
            }
            return View(this.products);
        }
        public IActionResult ById(int id)
        {
            var service = this.products.FirstOrDefault(s => s.Id == id);
            if (service == null)
            {
                return BadRequest();
            }
            return View(service);
        }

        public IActionResult AllAsJson()
        {
            var option = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            return Json(products, option);
        }

        public IActionResult AllAsText()
        {
            var text = new StringBuilder();
            foreach (var service in products)
            {
                text.AppendLine($"Sevice {service.Id}: {service.Name} - {service.Price}lv.");
            }
            return Content(text.ToString());
        }

        public IActionResult AllAsTextFile()
        {
            var text = new StringBuilder();
            foreach (var service in products)
            {
                text.AppendLine($"Sevice {service.Id}: {service.Name} - {service.Price}lv.");
            }

            Response.Headers.Add("content-disposition", "attachment; filename=products.txt");

            return File(Encoding.UTF8.GetBytes(text.ToString()), "text/plain");

        }
    }
}
