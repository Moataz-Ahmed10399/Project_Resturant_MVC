using System.ComponentModel.DataAnnotations;

namespace Project_Resturant_MVC.ViewModel
{
    public class VmCategory
    {
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 1000 characters")]
        [Required(ErrorMessage = "Category Description is  required")]
        public string? Description { get; set; }

    }
}
