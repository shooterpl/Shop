using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class Product
    {
        public int id { get; set; }
        [Display(Name = "Product Name")]
        public string name { get; set; }
        [Display(Name = "Quantity")]
        public int quantity { get; set; }
        [Display(Name = "Barcode Number")]
        public int barcode { get; set; }
    }
}