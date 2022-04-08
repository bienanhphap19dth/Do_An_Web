using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Do_An_Wed.Models;
using Do_An_Wed.Context;
using System.Security.Cryptography;
using System.Text;

namespace Do_An_Wed.Controllers
{
    public class HomeController : Controller
    {
        websitebanhang1Entities objwebsitebanhangEntities1 = new websitebanhang1Entities();
        private static string byte2String; // Biến Toàn cục //

        public ActionResult Index()
        {
            HomeModel objHomeModel = new HomeModel();
            objHomeModel.ListCategory = objwebsitebanhangEntities1.Category.ToList();
            objHomeModel.ListProduct = objwebsitebanhangEntities1.Product.ToList();

            return View(objHomeModel);
        }
        [HttpGet]
        public ActionResult Register()
        {
            
            return View(); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Users _user)
        {
            if(ModelState.IsValid)
            {
                var check = objwebsitebanhangEntities1.Users.FirstOrDefault(s => s.Email == _user.Email);
                if(check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    objwebsitebanhangEntities1.Configuration.ValidateOnSaveEnabled = false;
                    objwebsitebanhangEntities1.Users.Add(_user);
                    objwebsitebanhangEntities1.SaveChanges();
                    return RedirectToAction("Index");
                } 
                else
                {
                    ViewBag.error = "Email alrealy exists";
                    return View();
                }    
            }
          
            return View();
        }
        public static string GetMD5 (string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            for (int i =0; i< targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }
        [HttpGet]
        public ActionResult Login()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
           
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);
                var data = objwebsitebanhangEntities1.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                if(data.Count()>0)
                {
                    Session["FullName"] = data.FirstOrDefault().FristName + " " + data.FirstOrDefault().LastName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().Id;
                    return RedirectToAction("Index");
                }    
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }    
            }
            
            return View();
           
        }
        public ActionResult Logout()
        {
            Session.Clear();
            byte2String = null; // Khai báo biến toàn cục và Reset //
            return RedirectToAction("Login");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}