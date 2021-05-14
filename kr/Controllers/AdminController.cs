using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kr.Models;

namespace kr.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        [Authorize(Roles ="moder")]
        public ActionResult AdminMenu()
        {
            return View();
        }
        [Authorize(Roles = "moder")]
        public ActionResult Categories()
        {
            return View(dbContext.Categories.ToList());
        }
        [Authorize(Roles = "moder")]
        public ActionResult Products()
        {
            return View(dbContext.Products.Include(c => c.Category).ToList());
        }

        //действия для категорий
        [HttpGet]
        [Authorize(Roles = "moder")]
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "moder")]
        public ActionResult AddCategory(Category cat)
        {
            dbContext.Categories.Add(cat);
            dbContext.SaveChanges();
            return RedirectToAction("Categories","Admin");
        }
        [HttpGet]
        [Authorize(Roles = "moder")]
        public ActionResult EditCategory(int? id)
        {
            if(id==null) return RedirectToAction("Categories", "Admin");
            return View(dbContext.Categories.Where(c=>c.Id==id).FirstOrDefault());
        }
        [HttpPost]
        [Authorize(Roles = "moder")]
        public ActionResult EditCategory(Category cat)
        {
            dbContext.Entry(cat).State = EntityState.Modified;
            dbContext.SaveChanges();
            return RedirectToAction("Categories", "Admin");
        }
        [Authorize(Roles = "moder")]
        public ActionResult DeleteCategory(int? id)
        {
            if (id == null) return RedirectToAction("Categories", "Admin");
            Category cat= dbContext.Categories.Find(id);
            if (cat != null)
            {
                dbContext.Categories.Remove(cat);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Categories", "Admin");
        }

        //действия для товаров
        [HttpGet]
        [Authorize(Roles = "moder")]
        public ActionResult AddProduct()
        {
            AddProductsViewModel apvm = new AddProductsViewModel();
            apvm.Categories = dbContext.Categories.ToList();
            return View(apvm);
        }
        [HttpPost]
        [Authorize(Roles = "moder")]
        public ActionResult AddProduct(string title, string desc, int count, double price, HttpPostedFileBase upload,string CategoryTitle)
        {
            Product prod = new Product();
            prod.title = title;
            prod.desc = desc;
            prod.count = count;
            prod.price = price;
            if (upload != null)
            {
                // получаем имя файла
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                // сохраняем файл в папку Files в проекте
                upload.SaveAs(Server.MapPath("~/Images/" + fileName));
                prod.img = "/Images/" + fileName;
            }
            prod.CategoryId = dbContext.Categories.Where(c => c.title == CategoryTitle).FirstOrDefault().Id;
            dbContext.Products.Add(prod);
            dbContext.SaveChanges();
            return RedirectToAction("Products", "Admin");
        }
        [HttpGet]
        [Authorize(Roles = "moder")]
        public ActionResult EditProduct(int? id)
        {
            if (id == null) return RedirectToAction("Products", "Admin");
            AddProductsViewModel apvm = new AddProductsViewModel();
            apvm.Product = new Product();
            apvm.Product = dbContext.Products.Where(p => p.Id == id).FirstOrDefault();
           apvm.Categories = dbContext.Categories.ToList();
            return View(apvm);
        }
        [HttpPost]
        [Authorize(Roles = "moder")]
        public ActionResult EditProduct(int Id, string title, string desc, int count, double price, HttpPostedFileBase upload, string CategoryTitle)
        {
            Product prod = new Product();
            prod.Id = Id;
            prod.title = title;
            prod.desc = desc;
            prod.count = count;
            prod.price = price;
            if (upload != null)
            {
                // получаем имя файла
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                // сохраняем файл в папку Files в проекте
                upload.SaveAs(Server.MapPath("~/Images/" + fileName));
                prod.img = "/Images/" + fileName;
            }
            prod.CategoryId = dbContext.Categories.Where(c => c.title == CategoryTitle).FirstOrDefault().Id;
            dbContext.Entry(prod).State = EntityState.Modified;
            dbContext.SaveChanges();
            return RedirectToAction("Products", "Admin");
        }
        [Authorize(Roles = "moder")]
        public ActionResult DeleteProduct(int? id)
        {
            if (id == null) return RedirectToAction("Products", "Admin");
            Product prod = dbContext.Products.Find(id);
            if (prod != null)
            {
                dbContext.Products.Remove(prod);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Products", "Admin");
        }

        [Authorize(Roles = "moder")]
        public ActionResult Orders(int? id)
        {

            return View(dbContext.Orders.Include(o=>o.Product).ToList());
        }





        //действия с заказами
        //выполнение заказа
        [Authorize(Roles = "moder")]
        public ActionResult CompleetOrder(int? Id)
        {
            if (Id == null) return RedirectToAction("Orders", "Admin");
            Order order = dbContext.Orders.Include(p => p.Product).Where(o => o.Id == Id).FirstOrDefault();
            if (order != null)
            {
                order.isCompleet = true;
                Product pr = dbContext.Products.Find(order.Product.Id);
                pr.countOfSels += order.countProduct;
                dbContext.SaveChanges();
            }
            return RedirectToAction("Orders", "Admin");
        }

        //удаление заказа
        [Authorize(Roles = "moder")]
        public ActionResult DeleteOrder(int? id)
        {
            if (id == null) return RedirectToAction("Orders", "Admin");
            Order order = dbContext.Orders.Include(p => p.Product).Where(o => o.Id == id).FirstOrDefault();
            if (order != null)
            {
                if (order.isCompleet == false)
                {
                    Product pr = dbContext.Products.Find(order.Product.Id);
                    pr.count += order.countProduct;
                }
                dbContext.Orders.Remove(order);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Orders", "Admin");
        }
    }
}