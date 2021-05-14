using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kr.Models
{
    public class Category
    {
        public int Id {get;set;}
        [Required]
        [Display(Name = "Название")]
        public string title { get; set; }
        public List<Product> Products { get; set; }
    }
}