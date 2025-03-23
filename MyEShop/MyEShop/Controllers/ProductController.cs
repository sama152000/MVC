﻿using EShop.Manegers;
using EShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace EShop.Presentation.Controllers
{

    public class ProductController : Controller
    {
        private ProductManager ProductManager;
        private CategoryManager CategoryManager;
        public ProductController(ProductManager pmanager, CategoryManager cmanager)
        {
            ProductManager = pmanager;
            CategoryManager = cmanager;
        }

        public IActionResult Index(string searchText = "", decimal price = 0,
            int categoryId = 0, string vendorId = "", int pageNumber = 1,
            int pageSize = 3)
        {
            ViewData["CategoriesList"] = GetCategories();

            var list = ProductManager.Search(categoryId: categoryId, vendorId: vendorId,
                searchText: searchText, price: price, pageNumber: pageNumber, pageSize: pageSize);
            return View(list);
        }

        [Authorize(Roles = "Vendor,Admin")]
        [HttpGet]
        public IActionResult Add()
        {


            ViewData["CategoriesList"] = GetCategories();
          

            ViewBag.Title = "Welcome";
            return View();
        }
        [Authorize(Roles = "Vendor,Admin")]

        [HttpPost]
        public IActionResult Add(AddProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                
                foreach (var file in viewModel.Attachments)
                {
                    FileStream fileStream = new FileStream(
                            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Products", file.FileName),
                            FileMode.Create);

                    file.CopyTo(fileStream);

                    fileStream.Position = 0;

                    viewModel.Paths.Add($"/Images/Products/{file.FileName}");

                }
                viewModel.VendorId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                ProductManager.Add(viewModel.ToModel());

                return RedirectToAction("index");
            }

            ViewData["CategoriesList"] = GetCategories();
            return View();
        }
        private List<SelectListItem> GetCategories()
        {
            return CategoryManager.Get()
    .Select(cat => new SelectListItem(cat.Name, cat.Id.ToString())).ToList();
        }
    }
}