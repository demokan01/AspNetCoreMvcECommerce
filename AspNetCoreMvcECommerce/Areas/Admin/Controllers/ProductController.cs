using AspNetCoreMvcECommerce.Areas.Admin.Models.ViewModels;
using AspNetCoreMvcECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreMvcECommerce.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("admin")]
    [Route("admin/product")]
    public class ProductController : Controller
    {

        private DatabaseContext db = new DatabaseContext();

        public ProductController(DatabaseContext _db)
        {
            db = _db;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {

            ViewBag.Products = db.Products.ToList();
            return View();
        }

        [HttpGet]
        [Route("add")]
        public IActionResult Add()
        {
            var productViewModel = new ProductViewModel();
            productViewModel.Product = new Product();
            productViewModel.Categories = new List<SelectListItem>();
            var categories = db.Categories.ToList();
            foreach (var category in categories)
            {
                var group = new SelectListGroup { Name = category.Name };
                if (category.InverseParents != null && category.InverseParents.Count > 0)
                {
                    foreach (var subCategory in category.InverseParents)
                    {
                        var selectListItem = new SelectListItem()
                        {
                            Text = subCategory.Name,
                            Value = subCategory.Id.ToString(),
                            Group = group
                        };
                        productViewModel.Categories.Add(selectListItem);
                    }
                }

            }

            return View("Add", productViewModel);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(ProductViewModel productViewModel)
        {
            db.Products.Add(productViewModel.Product);
            db.SaveChanges();

            //Create default photo for new product
            var defaultPhoto = new Photo
            {
                Name = "no-image.jpg",
                Status = true,
                ProductId = productViewModel.Product.Id,
                Featured=true
            };
            db.Photos.Add(defaultPhoto);
            db.SaveChanges();

            return RedirectToAction("Index", "product", new { area = "admin" });
        }
    }
}
