using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Books.Models
{ // var conn1 = db.Database.Connection;
    public class Book
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Автор")]
        public string Author { get; set; }

        // [Range(typeof(decimal), "0.00", "49.99")]
        [Required(ErrorMessage = "Цена должна быть определена")]
        [Display(Name = "Цена")]
        public int Price { get; set; }

        [Required]
        [Display(Name = "Год")]
        [Range(1700, 2022, ErrorMessage = "Недопустимый год")]
        public int Year { get; set; }
    }
}