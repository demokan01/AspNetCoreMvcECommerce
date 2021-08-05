using AspNetCoreMvcECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreMvcECommerce.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("admin")]
    [Route("admin/category")]
    public class CategoryController : Controller
    {

        private DatabaseContext db = new DatabaseContext();

        public CategoryController(DatabaseContext _db)
        {
            db = _db;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            ViewBag.categories = db.Categories.Where(c=>c.Parent==null).ToList();
            return View();
        }

        [HttpGet]
        [Route("add")]
        public IActionResult Add()
        {
            var category = new Category();
            return View("Add",category);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(Category category)
        {
            category.Parent = null;
            db.Categories.Add(category);
            db.SaveChanges();
            return RedirectToAction("Index", "category", new { area = "admin" });
        }

        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index", "category", new { area = "admin" });
        }

        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var category = db.Categories.Find(id);
            return View("Edit", category);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public IActionResult Edit(int id,Category category)
        {
            var currentCategory = db.Categories.Find(id);
            currentCategory.Name = category.Name;
            currentCategory.Status = category.Status;
            db.SaveChanges();
            return RedirectToAction("Index", "category", new { area = "admin" });
        }

        [HttpGet]
        [Route("addsubcategory/{id}")]
        public IActionResult AddSubCategory(int id)
        {
            var subcategory = new Category()
            {
                ParentId = id
            };
            return View("AddSubCategory", subcategory);
        }

        [HttpPost]
        [Route("addsubcategory/{categoryId}")]
        public IActionResult AddSubCategory(int categoryId, Category subcategory)
        {
            db.Categories.Add(subcategory);
            db.SaveChanges();
            return RedirectToAction("Index", "category", new { area = "admin" });
        }
    }
}
