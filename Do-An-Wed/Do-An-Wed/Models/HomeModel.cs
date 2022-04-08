using Do_An_Wed.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Do_An_Wed.Models
{
    public class HomeModel
    {
        public List<Product> ListProduct { get; set; }
        public List<Category> ListCategory { get; set; }
    }
}