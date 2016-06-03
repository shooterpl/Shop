using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.ViewModels
{
    public class ProductViewModel
    {
        public int id { get; set; }
        [Display(Name = "Product Name")]
        public string name { get; set; }
        [Display(Name = "Quantity")]
        public int quantity { get; set; }
        [Display(Name = "Barcode Number")]
        public int barcode { get; set; }
        [Display(Name = "Price")]
        public float price { get; set; }
        public int orderedQuantity { get; set; }
    }
}