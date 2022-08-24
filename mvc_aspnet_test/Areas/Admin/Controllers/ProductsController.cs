using Microsoft.AspNetCore.Mvc;
using mvc_aspnet_test.Infrastructure;

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

        public IActionResult Index()
        {
            return View();
        }
    }
}
