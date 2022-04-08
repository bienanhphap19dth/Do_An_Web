using Do_An_Wed.Context;
using Do_An_Wed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Do_An_Wed.Controllers
{
    public class CartController : Controller
    {
        websitebanhang1Entities objwebsitebanhangEntities1 = new websitebanhang1Entities();
        // GET: Cart
        public ActionResult Index()
        {
            return View((List<CartModel>)Session["cart"]);
        }
        public ActionResult AddToCart(int Id, int quantity)
        {
            if(Session["cart"] == null)
            {
                List<CartModel> cart = new List<CartModel>();
                cart.Add(new CartModel { Product = objwebsitebanhangEntities1.Product.Find(Id), Quantity = quantity});
                Session["cart"] = cart;
                Session["count"] = 1;
            }  
            else
            {
                List<CartModel> cart = (List<CartModel >) Session["cart"];
                int index = isExist(Id);
                if(index != -1)
                {
                    cart[index].Quantity += quantity;
                }
                else
                {
                    cart.Add(new CartModel { Product = objwebsitebanhangEntities1.Product.Find(Id), Quantity = quantity });
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                }
                Session["cart"] = cart;
            }    
            return Json(new { Message = "Thành Công", JsonRequestBehavior.AllowGet});
        }
        private int isExist(int id)
        {
            List<CartModel> cart = (List<CartModel>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product.Id.Equals(id))
                    return i;
            return -1;
        }
        public ActionResult Remove(int Id)
        {
            List<CartModel> li = (List<CartModel>)Session["cart"];
            li.RemoveAll(x => x.Product.Id == Id);
            Session["cart"] = li;
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            return Json(new { Message = " Xóa Thành Công", JsonRequestBehavior.AllowGet });
        }
    }
}