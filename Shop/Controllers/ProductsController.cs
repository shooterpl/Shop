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
            foreach (Product model in productList)
            {
                ProductViewModel viewModel = new ProductViewModel();
                viewModel.name = model.name;
                viewModel.quantity = model.quantity;
                viewModel.barcode = model.barcode;
                viewModel.price = model.price;
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
            viewModel.price = model.price;
            viewModel.quantity = model.quantity;
            viewModel.barcode = model.barcode;
            viewModel.orderedQuantity = 1;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Buy(ProductViewModel viewModel)
        {
            List<Product> productList = db.Products.ToList();
            var model = productList.FirstOrDefault(x => x.name == viewModel.name);
            if (viewModel.orderedQuantity < 1) viewModel.orderedQuantity = 1;
            if (viewModel.orderedQuantity > model.quantity)
            {
                TempData["notice"] = "Sorry, number of product do you want is limmited due to fact that we do not have enought products in store now";
                viewModel.orderedQuantity = model.quantity;
            }
            TempData["summary"] = "Please accept your order";
            viewModel.name = model.name;
            viewModel.barcode = model.barcode;
            viewModel.quantity = model.quantity;
            viewModel.price = model.price;
            ViewBag.Total = model.price * viewModel.orderedQuantity;
            return View(viewModel);
        }


        [Authorize]
        public ActionResult Purchase(ProductViewModel viewModel)
        {
            List<Product> productList = db.Products.ToList();
            var model = productList.FirstOrDefault(x => x.name == viewModel.name);
            model.quantity -= viewModel.orderedQuantity;
            db.SaveChanges();
            return View();
        }

        public ActionResult ProductInfo(string name)
        {
            List<Product> productList = db.Products.ToList();
            ProductViewModel viewModel = new ProductViewModel();
            var model = productList.FirstOrDefault(x => x.name == name);
            viewModel.name = model.name;
            viewModel.barcode = model.barcode;
            viewModel.quantity = model.quantity;
            viewModel.price = model.price;
            return View(viewModel);
        }
    }
}