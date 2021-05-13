﻿using kr.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kr.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        public ActionResult Index(string product,string category )
        {
            IQueryable<Product> pr = dbContext.Products.Include(p => p.Category);
            if (!String.IsNullOrEmpty(category)&&category!="Все")
            {
                pr = pr.Where(p => p.Category.title == category);
            }
            if (!String.IsNullOrEmpty(product))
            {
                pr = pr.Where(p => p.title.Contains(product));
            }

            List<Category> categories = dbContext.Categories.ToList();
            categories.Insert(0, new Category { title = "Все", Id = 0 });

            MyIndexViewModel viewModel = new MyIndexViewModel();

            viewModel.products = pr.ToList();
                viewModel.categories = categories;
            if (!String.IsNullOrEmpty(product)) viewModel.selectname = product;
            if (!String.IsNullOrEmpty(category) && category != "Все") viewModel.selectcategory = category;
            
            return View(viewModel);
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
        [Authorize(Roles = "moder")]
        public ActionResult ToAdmin()
        {
            return RedirectToAction("AdminMenu", "Admin");
        }
        [Authorize(Roles = "moder,user")]
        [HttpGet]
        public ActionResult Buy(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Home");
            return View(dbContext.Products.Where(c=>c.Id==id).FirstOrDefault());
        }
        [Authorize(Roles = "moder,user")]
        [HttpPost]
        public ActionResult Buy(int id,int BuyCount)
        {
            Product pr = dbContext.Products.Where(p => p.Id == id).FirstOrDefault();
            pr.count -= BuyCount;
            Order order = new Order();
            order.orderDate = DateTime.Now;
            order.ProductId = id;
            order.Email = User.Identity.Name;
            order.countProduct = BuyCount;
            order.orderPrice = dbContext.Products.Where(p => p.Id == id).FirstOrDefault().price * (double)BuyCount;
            dbContext.Orders.Add(order);
            dbContext.SaveChanges();
            return RedirectToAction("EndOrder","Home");
        }
        public ActionResult EndOrder()
        {
            return View();
        }
    }
}