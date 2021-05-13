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

    }
}