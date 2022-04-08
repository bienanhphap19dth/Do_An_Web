using Do_An_Wed.Context;
using Do_An_Wed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Do_An_Wed.Controllers
{
    public class PaymentController : Controller
    {
        websitebanhang1Entities objwebsitebanhangEntities1 = new websitebanhang1Entities();
        // GET: Payment
        public ActionResult Index()
        {
            if(Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var lstCart = (List<CartModel>)Session["cart"];
                Order objOrder = new Order();
                objOrder.Name = "DonHang" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOrder.UserId = int.Parse(Session["idUser"].ToString());
                objOrder.CreateOnUtc = DateTime.Now;
                objOrder.Status = 1;
                objwebsitebanhangEntities1.Order.Add(objOrder);
                objwebsitebanhangEntities1.SaveChanges();
                int intOrderId = objOrder.Id;
                List<OrderDetail> lstOrderDetail = new List<OrderDetail>();
                foreach(var item in lstCart)
                {
                    OrderDetail obj = new OrderDetail();
                    obj.Quantity = item.Quantity;
                    obj.OrderId = intOrderId;
                    obj.ProductId = item.Product.Id;
                    lstOrderDetail.Add(obj);
                }
                objwebsitebanhangEntities1.OrderDetail.AddRange(lstOrderDetail);
                objwebsitebanhangEntities1.SaveChanges();
            }
            return View();
        }
    }
}