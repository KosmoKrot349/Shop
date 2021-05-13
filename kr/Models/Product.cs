using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace kr.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string title { get; set; }
        [Display(Name = "Описание")]
        public string desc { get; set; }
        [Display(Name = "Кол-во на складе")]
        public int count { get; set; }
        [Display(Name = "Цена за еденицу")]
        public double price { get; set; }
        [Display(Name = "Фото")]
        public string img { get; set; }
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Order> Orders { get; set; }
    }
}