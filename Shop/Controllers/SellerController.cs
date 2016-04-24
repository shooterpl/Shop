using Shop.Models;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class SellerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            List<ProductViewModel> viewModelList = new List<ProductViewModel>();
            List<Product> modelList = db.Products.ToList();
            foreach (Product model in modelList)
            {
                ProductViewModel viewModel = new ProductViewModel();
                viewModel.id = model.id;
                viewModel.name = model.name;
                viewModel.quantity = model.quantity;
                viewModel.barcode = model.barcode;
                viewModel.price = model.price;
                viewModelList.Add(viewModel);
            }
            return View(viewModelList);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(Product model)
        {
            db.Products.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
           
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string name)
        {
            Product model = db.Products.FirstOrDefault(x => x.name == name);
            ProductViewModel viewModel = new ProductViewModel();
            viewModel.id = model.id;
            viewModel.name = model.name;
            viewModel.quantity = model.quantity;
            viewModel.barcode = model.barcode;
            viewModel.price = model.price;
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(ProductViewModel viewModel)
        {
            try
            {
                Product model = db.Products.FirstOrDefault(x => x.id == viewModel.id);
                model.name = viewModel.name;
                model.price = viewModel.price;
                model.quantity = viewModel.quantity;
                model.barcode = viewModel.barcode;
                //SetUpdatableProperties(viewModel, model);
                //db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Product model = db.Products.FirstOrDefault(x => x.id == id);
            ProductViewModel viewModel = new ProductViewModel();
            viewModel.id = model.id;
            viewModel.name = model.name;
            viewModel.quantity = model.quantity;
            viewModel.barcode = model.barcode;
            viewModel.price = model.price;
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Product model = db.Products.FirstOrDefault(x => x.id == id);
                db.Products.Remove(model);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delivery(int id)
        {
            Product model = db.Products.FirstOrDefault(x => x.id == id);
            ProductViewModel viewModel = new ProductViewModel();
            viewModel.id = model.id;
            viewModel.name = model.name;
            viewModel.quantity = model.quantity;
            viewModel.barcode = model.barcode;
            viewModel.price = model.price;
            return View(viewModel);
        }
    }
}
