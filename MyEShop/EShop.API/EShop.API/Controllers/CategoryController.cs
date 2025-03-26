using EF_Core.Models;
using EShop.Manegers;
using EShop.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Api.Controllers
{
    [ApiController]
    [Route("api/{Controller}")]
    public class CategoryController : ControllerBase
    {

        private CategoryManager categoryManager;

        public CategoryController(CategoryManager _categoryManager)
        {
            categoryManager = _categoryManager;
        }

        [Route("index")]

        public IActionResult Index()
        {
            var list = categoryManager.Get().ToList();

            return Ok(list);
        }


        [HttpGet]
        [Route("Add")]
        public IActionResult Add()
        {
            return Ok();
        }



        [HttpPost]
        [Route("Add")]


        public IActionResult Add([FromBody] AddCategoryVM category)
        {
            var cat = category.ToCategoryEntity();

            categoryManager.Add(cat);
            return Ok(new { massage = "Successfull added" });
        }

        [HttpGet]
        [Route("Edit")]
        public IActionResult Edit(int Id, string name)
        {
            var selected = categoryManager.GetList(i => i.Id == Id).FirstOrDefault();
            return Ok(selected);
        }
        [HttpPost]
        [Route("Edit")]
        public IActionResult Edit(Category category)
        {
            categoryManager.Edit(category);
            return RedirectToAction("List");
        }

    }
}