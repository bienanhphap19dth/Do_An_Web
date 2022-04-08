using Do_An_Wed.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Do_An_Wed.Controllers
{
    public class CategoryController : Controller
    {
        websitebanhang1Entities objwebsitebanhangEntities1 = new websitebanhang1Entities();
        // GET: Category
        public ActionResult Index()
        {
            var lstCategory = objwebsitebanhangEntities1.Category.ToList();
            return View(lstCategory);
        }
        public ActionResult ProductCategory(int Id)
        {
            var listProduct = objwebsitebanhangEntities1.Product.Where(n => n.CategoryId == Id).ToList();
            return View(listProduct);
        }
    }
}