using AspNetCoreMvcECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreMvcECommerce.ViewComponents
{
    [ViewComponent(Name = "LatestProducts")]
    public class LatestProductsViewComponent : ViewComponent
    {
        private DatabaseContext db;
        public LatestProductsViewComponent(DatabaseContext _db)
        {
            this.db = _db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Product> products = ViewBag.LatestProducts = db.Products.OrderByDescending(p => p.Id).Where(p => p.Status).Take(2).ToList();
            return View("Index", products);
        }
    }
}
