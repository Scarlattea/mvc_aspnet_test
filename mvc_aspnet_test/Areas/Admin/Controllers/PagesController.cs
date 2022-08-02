﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc_aspnet_test.Infrastructure;
using mvc_aspnet_test.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc_aspnet_test.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PagesController : Controller
    {
        private readonly CmsShoppingCartContext context;
        public PagesController(CmsShoppingCartContext context)
        {
            this.context = context;
        }
        //GET /admin/pages
        public async Task<IActionResult> Index()
        {
            IQueryable<Page> pages = (from p in context.Pages orderby p.Sorting select p);
            List<Page> pagesList = await pages.ToListAsync();
            return View(pagesList);
        }

        //GET /admin/pages/details/5
        public async Task<IActionResult> Details(int id)
        {
            Page page = await context.Pages.FirstOrDefaultAsync(x => x.Id == id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }
    }
}