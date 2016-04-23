using Shop.Models;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Product
        public ActionResult Index()
        {
            List<Product> productList = db.Products.Where(x => x.quantity > 0).ToList();
            List<ProductViewModel> productViewModelList = new List<ProductViewModel>();
            foreach (Product product in productList)
            {
                ProductViewModel viewModel = new ProductViewModel();
                viewModel.name = product.name;
                viewModel.quantity = product.quantity;
                viewModel.barcode = product.barcode;
                productViewModelList.Add(viewModel);
            }
            return View(productViewModelList);
        }

        [Authorize]
        public ActionResult Buy(string name)
        {
            List<Product> productList = db.Products.ToList();
            var model = productList.FirstOrDefault(x => x.name == name);
            ProductViewModel viewModel = new ProductViewModel();
            viewModel.name = model.name;

            return View(viewModel);
        }

        [Authorize]
        public ActionResult Purchase(string name)
        {
            List<Product> productList = db.Products.ToList();
            ProductViewModel viewModel = new ProductViewModel();
            var model = productList.FirstOrDefault(x => x.name == name);
            model.quantity--;
            db.SaveChanges();
            viewModel.name = model.name;
            viewModel.barcode = model.barcode;
            viewModel.quantity = model.quantity;
            return View(viewModel);
        }

        public ActionResult ProductInfo(string name)
        {
            List<Product> productList = db.Products.ToList();
            ProductViewModel viewModel = new ProductViewModel();
            var model = productList.FirstOrDefault(x => x.name == name);
            viewModel.name = model.name;
            viewModel.barcode = model.barcode;
            viewModel.quantity = model.quantity;
            return View(viewModel);
        }
    }
}