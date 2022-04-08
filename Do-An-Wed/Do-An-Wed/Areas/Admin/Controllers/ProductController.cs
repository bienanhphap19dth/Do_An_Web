using Do_An_Wed.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Do_An_Wed.Models;
using System.IO;

using System.Data;
using System.Data.Entity;
using PagedList;
using static Do_An_Wed.ListtoDataTableConverter;

namespace Do_An_Wed.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        websitebanhang1Entities objwebsitebanhang1Entities = new websitebanhang1Entities();
        // GET: Admin/Product
        public ActionResult Index( string currentFilter, string SearchString , int? page)
        {
            var lstProduct = new List<Product>();
            if(SearchString !=null)
            {
                page = 1;

            }   
            else
            {
                SearchString = currentFilter;
            }    
            if(!string.IsNullOrEmpty(SearchString))
            {
                lstProduct = objwebsitebanhang1Entities.Product.Where(n => n.Name.Contains(SearchString)).ToList();
            }    
            else
            {
                lstProduct = objwebsitebanhang1Entities.Product.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int PageNumber = (page ?? 1);
            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();
            return View(lstProduct.ToPagedList(PageNumber, pageSize));
        }
        public ActionResult Details (int Id)
        {
            var objProduct = objwebsitebanhang1Entities.Product.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpGet]
        public ActionResult Create()
        {
            Common objCommon = new Common();
            var lstCat = objwebsitebanhang1Entities.Category.ToList();
            ListtoDataTableConverter Converter = new ListtoDataTableConverter();
            DataTable dtcategory = Converter.ToDataTable(lstCat);
            ViewBag.ListCategory = objCommon.ToSelectList(dtcategory, "Id", "Name");

            var lstBrand = objwebsitebanhang1Entities.Brand.ToList();
            DataTable dtBrand = Converter.ToDataTable(lstBrand);
            ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "Id", "Name");

            List<ProductType> lstProductType = new List<ProductType>();
            ProductType objProductType = new ProductType();
            objProductType.Id = 01;
            objProductType.Name = "Giảm Giá Sốc";
            lstProductType.Add(objProductType);

            objProductType = new ProductType();
            objProductType.Id = 02;
            objProductType.Name = "Đề Xuất";
            lstProductType.Add(objProductType);
            DataTable dtProductType = Converter.ToDataTable(lstProductType);
            ViewBag.ProductType = objCommon.ToSelectList(dtProductType, "Id", "Name");
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Product objProduct)
        {
                try
                {
                    if (objProduct.Avatar != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objProduct.Avatar.ToString());
                        string extension = Path.GetExtension(objProduct.Avatar.ToString());
                        fileName = fileName + extension;
                        objProduct.Avatar = fileName;
                        objProduct.Avatar.ToString();
                    }
                    objProduct.CreateOnUtc = DateTime.Now;
                    objwebsitebanhang1Entities.Product.Add(objProduct);
                    objwebsitebanhang1Entities.SaveChanges();

                }
                catch (Exception)
                {
                    return View();
                }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var objProduct = objwebsitebanhang1Entities.Product.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Delete(Product objpro)
        {
            var objProduct = objwebsitebanhang1Entities.Product.Where(n => n.Id == objpro.Id).FirstOrDefault();
            objwebsitebanhang1Entities.Product.Remove(objProduct);
            objwebsitebanhang1Entities.SaveChanges();
            
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var objProduct = objwebsitebanhang1Entities.Product.Where(n => n.Id == Id).FirstOrDefault();
            Common objCommon = new Common();
            var lstCat = objwebsitebanhang1Entities.Category.ToList();
            ListtoDataTableConverter Converter = new ListtoDataTableConverter();
            DataTable dtcategory = Converter.ToDataTable(lstCat);
            ViewBag.ListCategory = objCommon.ToSelectList(dtcategory, "Id", "Name");

            var lstBrand = objwebsitebanhang1Entities.Brand.ToList();
            DataTable dtBrand = Converter.ToDataTable(lstBrand);
            ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "Id", "Name");
            List<ProductType> lstProductType = new List<ProductType>();
            ProductType objProductType = new ProductType();
            objProductType.Id = 01;
            objProductType.Name = "Giảm Giá Sốc";
            lstProductType.Add(objProductType);

            objProductType = new ProductType();
            objProductType.Id = 02;
            objProductType.Name = "Đề Xuất";
            lstProductType.Add(objProductType);
            DataTable dtProductType = Converter.ToDataTable(lstProductType);
            ViewBag.ProductType = objCommon.ToSelectList(dtProductType, "Id", "Name");
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Edit(int Id, Product objProduct)
        {
            if(objProduct.Avatar != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objProduct.Avatar.ToString());
                string extension = Path.GetExtension(objProduct.Avatar.ToString());
                fileName = fileName + extension;
                objProduct.Avatar = fileName;
                objProduct.Avatar.ToString();
            }
            objwebsitebanhang1Entities.Entry(objProduct).State = EntityState.Modified;
            objwebsitebanhang1Entities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}