using AspNetCoreMvcECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreMvcECommerce.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {

        private DatabaseContext db = new DatabaseContext();

        public HomeController(DatabaseContext _db)
        {
            db = _db;
        }

        [Route("")]
        [Route("index")]
        [Route("~/")]
        public IActionResult Index()
        {
            ViewBag.isHome = true;
            var featuredProducts = db.Products.OrderByDescending(p => p.Id).Where(p => p.Status && p.Featured).ToList();
            ViewBag.FeaturedProducts = featuredProducts;
            ViewBag.CountFeaturedProducts = featuredProducts.Count;
            ViewBag.LatestProducts = db.Products.OrderByDescending(p => p.Id).Where(p => p.Status).Take(9).ToList(); 
            return View();
        }
    }
}
