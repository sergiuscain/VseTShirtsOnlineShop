﻿using System.ComponentModel.DataAnnotations;

namespace VseTShirts.Areas.Admin.Models
{
    public class ProductEditViewModel
    {
        public Guid Id { get; set; }   //уникальный идентификатор

        [Required(ErrorMessage = "Обязательное поле")]
        [MinLength(4, ErrorMessage = "Слишком короткое название")]
        public string Name { get; set; }  //название товара

        [Required(ErrorMessage = "Обязательное поле")]
        [MinLength(10, ErrorMessage = "Слишком короткое описание")]
        public string Description { get; set; } //описание товара

        [Required(ErrorMessage = "Обязательное поле")]
        [Range(100, 99000, ErrorMessage = "Некорректная цена")]
        public decimal Price { get; set; }  //цена

        [Required(ErrorMessage = "Обязательное поле")]
        [Range(1, 10000, ErrorMessage = "Некорректное количество товара")]
        public int Quantity { get; set; } //количество

        [Required(ErrorMessage = "Обязательное поле")]
        public string Sex { get; set; } //себестоимость

        [Required(ErrorMessage = "Обязательное поле")]
        [MinLength(4, ErrorMessage = "Слишком короткое название категрии")]
        public string Category { get; set; } //категория товара

        [Required(ErrorMessage = "Обязательное поле")]
        [MinLength(4, ErrorMessage = "Неправильное название цвета")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(4, ErrorMessage = "Введите размер одежды(XS/X/M/L/XL/XXL/XXXL)")]
        public string Size { get; set; }
        public string NameOfCollection { get; set; }
        [Required(ErrorMessage = "Загрузите новую картинку")]
        public List<string> ImagePaths { get; set; }
        public List<IFormFile> UploadedFiles { get; set; }
    }
}
