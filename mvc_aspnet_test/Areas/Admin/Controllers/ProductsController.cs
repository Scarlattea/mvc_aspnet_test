using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mvc_aspnet_test.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace mvc_aspnet_test.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly CmsShoppingCartContext context;
        public ProductsController(CmsShoppingCartContext context)
        {
            this.context = context;
        }

        // GET /admin/products
        public async Task<IActionResult> Index()
        {
            return View(await context.Products.OrderByDescending(x => x.Id).Include(x=> x.Category).ToListAsync());
        }
        //GET /admin/products/create
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(context.Categories.OrderBy(x=> x.Sorting), "Id", "Name");

            return View();
        }
    }
}
