using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace kr.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Название")]
        public string title { get; set; }
        [Display(Name = "Описание")]
        public string desc { get; set; }
        [Required]
        [Range(0, 9999999, ErrorMessage = "Не кореектное значение поля")]
        [Display(Name = "Кол-во на складе")]
        public int count { get; set; }
        [Required]
        [Range(0, 9999999.99)]
        [Display(Name = "Цена за еденицу")]
        public double price { get; set; }

        [Display(Name = "Фото")]
        public string img { get; set; }
        [Display(Name = "Продано")]
        public int countOfSels { get; set; }
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Order> Orders { get; set; }
    }
}