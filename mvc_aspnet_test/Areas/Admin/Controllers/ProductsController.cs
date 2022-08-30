using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mvc_aspnet_test.Infrastructure;
using mvc_aspnet_test.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PagedList.Mvc;
using PagedList;
/*using System.Web.Mvc;*/

namespace mvc_aspnet_test.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly CmsShoppingCartContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductsController(CmsShoppingCartContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET /admin/products
        public async Task<IActionResult> Index(int? page, int p = 1)
        {
            /*int pageSize = 3;
             var products = context.Products.OrderByDescending(x => x.Id).Include(x=> x.Category).Skip((p-1)*pageSize).Take(pageSize);*/

            var products = context.Products.OrderByDescending(x => x.Id).Include(x => x.Category);

            /*ViewBag.PageNumber = p;*/
            /*ViewBag.PageRange = pageSize;*/
            /*ViewBag.TotalPages = (int)Math.Ceiling((decimal)context.Products.Count() / pageSize);*/

            /*return View(await products.ToListAsync());*/
            return View(products.ToList().ToPagedList(page ?? 1,3));

        }
        //GET /admin/products/create
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(context.Categories.OrderBy(x=> x.Sorting), "Id", "Name");

            return View();
        }

        //POST /admin/products/create
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Slug = product.Name.ToLower().Replace(" ", "-");

                var slug = await context.Products.FirstOrDefaultAsync(x => x.Slug == product.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "The product already exists.");
                    return View(product);
                }

                string imageName = "noimage.png";
                if(product.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(webHostEnvironment.WebRootPath, "media/products");
                    imageName = Guid.NewGuid().ToString()+"_"+product.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadsDir, imageName);
                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await product.ImageUpload.CopyToAsync(fs);
                    fs.Close();
                }

                product.Image = imageName;

                context.Add(product);
                await context.SaveChangesAsync();

                TempData["Success"] = "The product has been added";

                return RedirectToAction("Index");
            }
            return View(product);
        }
    }
}
