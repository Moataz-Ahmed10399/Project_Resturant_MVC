using System.ComponentModel.DataAnnotations;

namespace Project_Resturant_MVC.ViewModel
{
    public class VmOrderItem
    {
        [Required(ErrorMessage = "Menu Item is required")]
        public int MenuItemId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100")]
        public int Quantity { get; set; }
    }
}
