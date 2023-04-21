using Microsoft.AspNetCore.Mvc;
using MyApp.DataAccess.Infrastructure.IRepository;
using MyApp.Models.ViewModel;

namespace MyWebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private IUnitOfWork _unitofWork;
        private IWebHostEnvironment _hostingEnvironment;

        public ProductController(IUnitOfWork unitofWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitofWork = unitofWork;
            _hostingEnvironment = hostingEnvironment;
        }


        #region ApiCall

        public IActionResult AllProducts()
        {
            var allproducts = _unitofWork.Product.GetAll(includeProperties: "Category");
            return Json(new { Data = allproducts });
        }

        #endregion



        public IActionResult Index()
        {
            //ProductVM productvm = new ProductVM();
            //productvm.Products = _unitofWork.Product.GetAll();
            return View();
        }
        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitofWork.Category.Add(category);
        //        _unitofWork.Save();
        //        TempData["success"] = "Category Created Successfully";
        //        return RedirectToAction("Index");
        //    }
        //    return View(category);

        //}
        [HttpGet]
        public IActionResult CreateUpdate(int? id)
        {
            ProductVM vm = new ProductVM()
            {
                product = new(),
                Categories = _unitofWork.Category.GetAll().Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })


            };

            if (id == null || id == 0)
            {
                return View(vm);
            }
            else
            {
                vm.product = _unitofWork.Product.GetT(x => x.Id == id);
                if (vm.product == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(vm);

                }

            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(ProductVM vm, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string fileName = String.Empty;
                if (file != null)
                {
                    string uploadDir = Path.Combine(_hostingEnvironment.WebRootPath, "ProductImage");
                    fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                    string filePath = Path.Combine(uploadDir, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    vm.product.ImageUrl = @"\ProductImage\" + fileName;
                }
                if (vm.product.Id == 0)
                {
                    _unitofWork.Product.Add(vm.product);

                }





                _unitofWork.Save();

                return RedirectToAction("Index");
            }
            return View("Index");

        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _unitofWork.Category.GetT(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteData(int? id)
        {
            var category = _unitofWork.Category.GetT(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _unitofWork.Category.Delete(category);
            _unitofWork.Save();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");

        }
    }
}
