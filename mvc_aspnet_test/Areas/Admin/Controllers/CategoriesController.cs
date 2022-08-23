﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc_aspnet_test.Infrastructure;
using mvc_aspnet_test.Models;
using System.Linq;
using System.Threading.Tasks;

namespace mvc_aspnet_test.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly CmsShoppingCartContext context;
        public CategoriesController(CmsShoppingCartContext context)
        {
            this.context = context;
        }

        // GET /admin/categories
        public async Task<IActionResult> Index()
        {
            return View(await context.Categories.OrderBy(x=> x.Sorting).ToListAsync());
        }


        //GET /admin/categories/create
        public IActionResult Create() => View();

        //POST /admin/categories/create
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = category.Name.ToLower().Replace(" ", "-");
                category.Sorting = 100;

                var slug = await context.Categories.FirstOrDefaultAsync(x => x.Slug == category.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "The category already exists.");
                    return View(category);
                }
                context.Add(category);
                await context.SaveChangesAsync();

                TempData["Success"] = "The category has been added";

                return RedirectToAction("Index");
            }
            return View(category);
        }
    }
}
