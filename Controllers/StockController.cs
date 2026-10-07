using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Site_Cadastro.Data;
using Site_Cadastro.Models;

namespace Site_Cadastro.Controllers
{
    public class StockController : Controller
    {
        private readonly AppDbContext _context;

        public StockController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult AddStock()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStock(string product, string recivedPrice, int quantities)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (userIdString == null)
            {
                return RedirectToAction("Users","Signup");
            }
            
            double price ;
            if(product == null)
            {
                ViewBag.Error = "Produto sem nome";
                return View();
                    
            }

            if (!double.TryParse(recivedPrice, out price))
            {
                ViewBag.Error= "Digite apenas números";
                return View();
            }
            if (quantities <= 0)
            {
                ViewBag.Error = "adicionama quantidade valída de produtos";
                return View();
            }
                
                
            var userId = int.Parse(userIdString);
            var Products = new Stock
            {
                Product = product,
                Price = price,
                Quantities = quantities,
                UserId = userId
            };
            _context.Stocks.Add(Products);
            _context.SaveChanges();
            return RedirectToAction("HomeApp", "App");
            
        }

        
        public IActionResult Dashboard()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null)
            {
                return RedirectToAction("Signin", "Users");
            }
            var userId = int.Parse(userIdString);
            var products = _context.Stocks
            .Where(w=> w.UserId == userId).ToList();

            return View(products);
        }

        
        public IActionResult MarketPlace()
        {

            var listProducts = _context.Stocks.ToList();
            
            return View(listProducts);
        }

        public IActionResult ProductDetails(int id)
        {
            var product = _context.Stocks.FirstOrDefault(p=> p.Id == id);

            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }

       
        public IActionResult RemoveStock(int id )
        {
            var product = _context.Stocks.FirstOrDefault(p => p.Id == id);
            _context.Stocks.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }


        
        
        public IActionResult UpdateStockProducts(int id)
        {
            var product = _context.Stocks.FirstOrDefault(p=> p.Id == id);

            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult UpdateStock(int id, int quantities)
        {
            var product = _context.Stocks.FirstOrDefault(p => p.Id == id);
            if((product.Quantities + quantities) <=0)
            {
                ViewBag.Error ="Alteração de estoque com valor Não permitido" ;
                return View("UpdateStockProducts", product);
            }
            product.Quantities += quantities;
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

    }
}
