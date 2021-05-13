using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kr.Models
{
    public class MyIndexViewModel
    {
        public List<Product> products { get; set; }
        public List<Category> categories { get; set; }
        public string selectname { get; set; }
        public string selectcategory { get; set; }

        public List<string> sorts = new List<string> { "По убыванию", "По возрастанию" };
        public List<string> sortTypes = new List<string> { "По цене", "По названию", "По кол-ву продаж" };
        public string selectSort { get; set; }
        public string selectTypeOfSort { get; set; }

    }
}