using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Resturant_MVC.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Resturant_MVC.ViewModel
{
    public class VmMenuItem
    {
        [Required(ErrorMessage = "MenuItem name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 1000 characters")]
        [Required(ErrorMessage = "MenuItem Description is  required")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(1, 999999.99, ErrorMessage = "Price must be between $1 and $999,999.99")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Quantity is requried")]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }


        [Required(ErrorMessage = "Category is requried")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public SelectList? Categories { get; set; }


        [Required(ErrorMessage = "PreparationTimeMinutes is requried")]
        [Range(1, 300)]
        public int PreparationTimeMinutes { get; set; }
       
        
        public string? ImagePath { get; set; }
        [NotMapped]

        public IFormFile? ImageFile { get; set; }


    }
}
