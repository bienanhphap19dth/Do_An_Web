using Do_An_Wed.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Do_An_Wed.Models
{
    public class CartModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}