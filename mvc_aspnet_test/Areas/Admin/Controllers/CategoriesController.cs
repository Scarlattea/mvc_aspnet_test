using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc_aspnet_test.Infrastructure;
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
    }
}
