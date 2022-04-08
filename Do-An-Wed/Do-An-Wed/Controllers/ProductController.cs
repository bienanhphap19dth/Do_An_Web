using Do_An_Wed.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Do_An_Wed.Controllers
{
    public class ProductController : Controller
    {
        websitebanhang1Entities objwebsitebanhangEntities1 = new websitebanhang1Entities();
        // GET: Product
        public ActionResult Detail(int Id)
        {
            var objProduct = objwebsitebanhangEntities1.Product.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
    }
}