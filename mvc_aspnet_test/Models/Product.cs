﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvc_aspnet_test.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required, MinLength(2, ErrorMessage = "Minumum length is 2")]
        public string Name { get; set; }
        public string Slug { get; set; }
        [Required, MinLength(4, ErrorMessage = "Minumum length is 4")]
        public string Description { get; set; }
        [Column(TypeName ="decimal(18,2)")]
        public decimal Price { get; set; }
        [Display(Name="Category")]
        public int CategoryId { get; set; }
        public string Image { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        [NotMapped]
        public IFormFile ImageUpload { get; set; }
    }
}
